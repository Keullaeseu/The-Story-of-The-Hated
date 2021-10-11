using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using System.Threading;
using System.Threading.Tasks;

public class NetworkPlayerLobby : NetworkBehaviour
{
    #region Var
    // GameMangerMenu for not abuse "GameObject.Find"
    [SerializeField] private GameObject gameManagerMenuUI = null;
    [SerializeField] private LobbyPlayerMenu lobbyPlayerMenu = null;
    [SerializeField] NetworkIdentity clientNetworkIdentity = null;

    [SerializeField] private byte[] profileImageByte = null;

    [SyncVar(hook = nameof(HandleDisplayNameChanged))]
    public string DisplayName = "Loading...";
    [SyncVar(hook = nameof(HandleReadyStatusChanged))]
    public bool IsReady = false;

    // Use for mark host in lobby
    [SyncVar]
    public bool isHost = false;

    [SyncVar]
    [Scene] [SerializeField] private string MissionScene = string.Empty;

    #region GameValueInitialization

    // Player team
    [SyncVar]
    public int Team = -1;
    // Team leader (Not a host but who is king\queen by sides)
    [SyncVar]
    public bool TeamLeader = false;
    [SyncVar]
    public int Slot = -1;

    #endregion

    // Take the lobby for future use
    private NetworkManagerGame lobby;
    private NetworkManagerGame Lobby
    {
        get
        {
            if (lobby != null) { return lobby; }
            return lobby = NetworkManager.singleton as NetworkManagerGame;
        }
    }

    #endregion

    #region Server

    public override void OnStartServer()
    {
        // Do only for local host
        if (isLocalPlayer)
        {
            // Set host
            isHost = true;
            // Change check for team leader in next updates, for now host is a team leader - 1
            TeamLeader = true;
        }
    }

    #region ClientRpc && TargetRpc

    [TargetRpc]
    private void RpcSendAvatarToClient(NetworkConnection target, byte[] bytes)
    {
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);
        tex.Apply();
        Lobby.LobbyPlayerProfileImage.Add(tex);
    }

    [TargetRpc]
    public void RpcRemoveAvatarAt(NetworkConnection target, int index)
    {
        Lobby.LobbyPlayerProfileImage.RemoveAt(index);
    }

    [ClientRpc]
    private void RpcUpdateLobby()
    {
        UpdateLobbyMenu();
    }

    #endregion

    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        // Find menu manager for UI
        gameManagerMenuUI = GameObject.Find("GameManager");

        if (isHost)
        {
            // UI initialization for server
            gameManagerMenuUI.GetComponent<GameManagerMenu>().StartGameButton.SetActive(true);
            // Set mission for host
            MissionScene = Lobby.MissionScene;
        }
        else  // Take mission from host
        {
            MissionScene = Lobby.LobbyPlayers[0].MissionScene;

            // Turn on mission UI
            if (MissionScene != string.Empty)
            {
                List<GameObject> missionList = gameManagerMenuUI.GetComponent<GameManagerMenu>().MissionList;

                for (int i = 0; i < missionList.Count; i++)
                {
                    if (missionList[i].GetComponent<Mission>().GetMissionScene() == MissionScene)
                    {
                        gameManagerMenuUI.GetComponent<GameManagerMenu>().SetMission(i);
                    }
                }
            }
        }

        // Take lobby player menu for change: avatar, name e.t.c.
        lobbyPlayerMenu = gameManagerMenuUI.GetComponent<GameManagerMenu>().LobbyPlayersMenuUI.GetComponent<LobbyPlayerMenu>();

        // Get ready button and add lister to it
        Button readyButton = gameManagerMenuUI.GetComponent<GameManagerMenu>().ReadyButton.GetComponent<Button>();
        readyButton.onClick.AddListener(CmdSetReady);

        // Take name from local var and send to server
        CmdSetDisplayName(ProfileInitialization.DisplayName);

        // Encode client avatar into byte array
        profileImageByte = ProfileInitialization.ProfileImage.EncodeToPNG();

        // Send to server byte array of avatar
        CmdSendAvatar(profileImageByte);

        // Get avatar from host
        if (!isHost && Lobby.LobbyPlayersNetId.Count > Lobby.LobbyPlayerProfileImage.Count)
        {
            CmdGetAvatar(Lobby.LobbyPlayerProfileImage.Count, clientNetworkIdentity.netId);
        }

        // Do only for clients 
        if (hasAuthority && !isHost)
        {
            gameManagerMenuUI.GetComponent<GameManagerMenu>().ConnectMenuUI.SetActive(false);

            gameManagerMenuUI.GetComponent<GameManagerMenu>().ClientInitialization();
        }

        // Initialization for in game use
        GameValueInitialization();
    }

    public override void OnStartClient()
    {
        // Add player to lobby player list
        Lobby.LobbyPlayers.Add(this);
        // Add netId to player list
        Lobby.LobbyPlayersNetId.Add(clientNetworkIdentity.netId);

        // Update lobby menu UI
        UpdateLobbyMenu();
    }

    public override void OnStopClient()
    {
        // Remove player from lobby player list
        Lobby.LobbyPlayers.Remove(this);
        // Remove netId from player list
        Lobby.LobbyPlayersNetId.Remove(clientNetworkIdentity.netId);



        if (isLocalPlayer && !isHost)
        {
            // Return to connection menu
            gameManagerMenuUI.GetComponent<GameManagerMenu>().LobbyMenuUI.SetActive(false);
            gameManagerMenuUI.GetComponent<GameManagerMenu>().ConnectMenuUI.SetActive(true);
        }

        // Update lobby menu UI
        UpdateLobbyMenu();
    }

    #region Command

    [Command]
    private void CmdSetDisplayName(string displayName)
    {
        DisplayName = displayName;
    }

    [Command]
    private void CmdSetTeam(int team)
    {
        Team = team;
    }

    [Command]
    private void CmdSetTeamLeader(bool teamLeader)
    {
        TeamLeader = teamLeader;
    }

    [Command]
    private void CmdSetReady()
    {
        IsReady = !IsReady;
    }

    [Command]
    private void CmdSendAvatar(byte[] bytes)
    {
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);
        tex.Apply();
        Lobby.LobbyPlayerProfileImage.Add(tex);
    }

    [Command]
    private void CmdGetAvatar(int clientPlayerProfileImageCount, uint playerUint)
    {
        // Get player netId for send him later
        //        int playerIndexNetId = Lobby.LobbyPlayersNetId.IndexOf(playerUint);

        // For every new avatar in client list get a missing
        for (int imageCount = clientPlayerProfileImageCount; imageCount < Lobby.LobbyPlayersNetId.Count; imageCount++)
        {
            RpcSendAvatarToClient(Lobby.LobbyPlayersNetworkConnection[Lobby.LobbyPlayersNetId.IndexOf(playerUint)], Lobby.LobbyPlayerProfileImage[imageCount].EncodeToPNG());
        }

        // Update for other player's avatar
        for (int index = 1; index < Lobby.LobbyPlayersNetworkConnection.Count - 1; index++)
        {
            RpcSendAvatarToClient(Lobby.LobbyPlayersNetworkConnection[index], Lobby.LobbyPlayerProfileImage[Lobby.LobbyPlayerProfileImage.Count - 1].EncodeToPNG());
        }

        // Send update lobby UI for all client include host
        RpcUpdateLobby();
    }

    #endregion

    #endregion

    #region Void's

    // Update lobby if any of players changer ready state
    public void HandleReadyStatusChanged(bool oldValue, bool newValue) => UpdateLobbyMenu();
    // Update lobby if display name change
    public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateLobbyMenu();

    private void UpdateLobbyMenu()
    {
        if (!hasAuthority)
        {
            // For each not local lobby
            foreach (var player in Lobby.LobbyPlayers)
            {
                if (player.hasAuthority)
                {
                    player.UpdateLobbyMenu();
                    break;
                }
            }
            return;
        }

        // Update all lobby player menu UI to null, but not for already used lobby slot's
        for (int i = Lobby.LobbyPlayersNetId.Count; i < Lobby.maxConnections - 1; i++)
        {
            lobbyPlayerMenu.playerNameTexts[i].text = string.Empty;
            lobbyPlayerMenu.playerReadyTexts[i].text = string.Empty;
            lobbyPlayerMenu.playerProfileImage[i].texture = null;
        }

        // Update all lobby player menu UI for local player
        for (int i = 0; i < Lobby.LobbyPlayersNetId.Count; i++)
        {
            lobbyPlayerMenu.playerNameTexts[i].text = lobby.LobbyPlayers[i].DisplayName;
            lobbyPlayerMenu.playerReadyTexts[i].text = lobby.LobbyPlayers[i].IsReady ?
                "<color=green>Ready</color>" :
                "<color=red>Not Ready</color>";
            if (lobby.LobbyPlayers.Count == lobby.LobbyPlayerProfileImage.Count)
            {
                lobbyPlayerMenu.playerProfileImage[i].texture = lobby.LobbyPlayerProfileImage[i];
            }
        }
    }

    private void GameValueInitialization()
    {
        // In next updates add team lists and checks for team leaders
        Team = 1;

        // Send to server if client and not host
        if (hasAuthority && !isHost)
        {
            CmdSetTeam(Team);
            CmdSetTeamLeader(TeamLeader);
        }
    }

    #endregion    

}
