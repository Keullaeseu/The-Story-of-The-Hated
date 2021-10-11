using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class NetworkManagerGame : NetworkManager
{
    // Do in next updates: Check for an instnace of NetworkManager (it can dublicate after a game) 

    [Header("Lobby")]
    [Scene] [SerializeField] private string menuScene = string.Empty;
    [Scene] public string MissionScene = string.Empty;
    [SerializeField] private int minPlayersToStart = 2;
    [SerializeField] private GameManagerMenu gameManagerMenuUI = null;

    public List<NetworkConnection> LobbyPlayersNetworkConnection = new List<NetworkConnection>();
    public List<uint> LobbyPlayersNetId = new List<uint>();
    public List<NetworkPlayerLobby> LobbyPlayers = new List<NetworkPlayerLobby>();
    public List<Texture2D> LobbyPlayerProfileImage = new List<Texture2D>();

    [Header("Game")]
    [SerializeField] private NetworkPlayerGame gamePlayerPrefab = null;
    [SerializeField] private GameObject playerAvatarPrefab = null;

    public List<NetworkPlayerGame> GamePlayers = new List<NetworkPlayerGame>();

    // Event for player spawn system
    public static event Action<NetworkConnection> OnServerReadied;

    public override void Start()
    {
        if (gameManagerMenuUI != null) { return; }

        // Fix while ofline scene
        gameManagerMenuUI = GameObject.Find("GameManager").GetComponent<GameManagerMenu>();
    }

    #region Server

    public override void OnServerConnect(NetworkConnection conn)
    {
        // if main menu
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            if (numPlayers >= maxConnections)
            {
                conn.Disconnect();
                return;
            }

            LobbyPlayersNetworkConnection.Add(conn);
        }
        else
        {
            // Disconnect player, we are in the game now
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // if main menu
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            if (conn.identity != null)
            {
                // Return if players <= 1
                if (LobbyPlayersNetId.Count <= 1)
                {
                    return;
                }

                var player = conn.identity.GetComponent<NetworkPlayerLobby>();

                LobbyPlayersNetworkConnection.Remove(conn);

                // Remove NetworkPlayerLobby from list of lobby players
                LobbyPlayers.Remove(player);

                // Find index of user by it's netId and delete
                int playerDisconnectedIndex = LobbyPlayersNetId.IndexOf(conn.identity.netId);
                LobbyPlayerProfileImage.RemoveAt(playerDisconnectedIndex);

                // Delete avatar from player list
                for (int playerIndex = 1; playerIndex < LobbyPlayersNetworkConnection.Count; playerIndex++)
                {
                    LobbyPlayers[playerIndex].RpcRemoveAvatarAt(LobbyPlayersNetworkConnection[playerIndex], playerDisconnectedIndex);
                }

                // Remove netId from list of lobby players
                LobbyPlayersNetId.Remove(conn.identity.netId);
            }
        }

        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        LobbyPlayersNetworkConnection.Clear();

        LobbyPlayers.Clear();
        LobbyPlayersNetId.Clear();
        LobbyPlayerProfileImage.Clear();

        // Clear dublicate of network manager
        if (SceneManager.GetActiveScene().path != menuScene)
        {
            offlineScene = menuScene;
            base.OnStopServer();
        }
    }

    public override void ServerChangeScene(string newSceneName)
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            for (int i = LobbyPlayers.Count - 1; i >= 0; i--)
            {
                var conn = LobbyPlayers[i].connectionToClient;
                var gamePlayerInstance = Instantiate(gamePlayerPrefab);

                // Set displayName\Team\TeamLeader from lobby to new player gameobject
                gamePlayerInstance.SetDisplayName(LobbyPlayers[i].DisplayName);
                gamePlayerInstance.SetTeam(LobbyPlayers[i].Team);
                gamePlayerInstance.SetTeamLeader(LobbyPlayers[i].TeamLeader);

                // Need update with team sides in nex updates
                gamePlayerInstance.SetSlot(i);

                NetworkServer.Destroy(conn.identity.gameObject);

                NetworkServer.ReplacePlayerForConnection(conn, gamePlayerInstance.gameObject);
            }
        }

        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        if (SceneManager.GetActiveScene().path != menuScene)
        {
            for (int i = GamePlayers.Count - 1; i >= 0; i--)
            {
                var conn = GamePlayers[i].connectionToClient;
                var playerAvatarPrefabInstance = Instantiate(playerAvatarPrefab);

                // Set team
                playerAvatarPrefabInstance.GetComponent<Unit>().SetTeam(GamePlayers[i].GetTeam());

                // Find spawn points for players
                GameObject spawnPointsGameObject = GameObject.FindGameObjectWithTag("SpawnPoints");
                // Start postion for player avatar
                playerAvatarPrefabInstance.transform.position = spawnPointsGameObject.GetComponent<SpawnPoints>().SpawnPointsArray[i].transform.position;

                // Spawn player avatar
                NetworkServer.Spawn(playerAvatarPrefabInstance, conn);
            }
        }

        base.OnServerSceneChanged(sceneName);
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);

        OnServerReadied?.Invoke(conn);
    }

    #region Void's

    public void StartGame()
    {
        // If in main menu
        if (SceneManager.GetActiveScene().path == menuScene && LobbyPlayersNetworkConnection.Count >= minPlayersToStart)
        {
            // Check all in lobby is ready
            for (int i = 0; i < LobbyPlayers.Count; i++)
            {
                // Return if player not ready
                if (!LobbyPlayers[i].IsReady) { return; }
            }

            ServerChangeScene(MissionScene);
        }
    }

    #endregion

    #endregion

    #region Client

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        LobbyPlayersNetworkConnection.Clear();

        LobbyPlayers.Clear();
        LobbyPlayersNetId.Clear();
        LobbyPlayerProfileImage.Clear();

        // If in main menu
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            CampainUIDefaultState();
        }
        else
        {
            offlineScene = menuScene;
        }

        base.OnClientDisconnect(conn);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);

        // If in game
        if (SceneManager.GetActiveScene().path != menuScene)
        {
            // After scene changed turn on player's UI
            for (int i = GamePlayers.Count - 1; i >= 0; i--)
            {
                GamePlayers[i].Initialization();
            }
        }
    }

    private void CampainUIDefaultState()
    {
        gameManagerMenuUI.PlayBackButton.SetActive(true);
        gameManagerMenuUI.PlayCancelCoopButton.SetActive(false);
        gameManagerMenuUI.LobbyPlayersMenuUI.SetActive(false);
        gameManagerMenuUI.PlaySinglePlayerButton.SetActive(true);
        gameManagerMenuUI.ReadyButton.SetActive(false);
        gameManagerMenuUI.PlayCoopButton.SetActive(true);
        gameManagerMenuUI.StartGameButton.SetActive(false);
    }

    #endregion

}
