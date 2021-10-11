using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Mirror;

public class ClientConnect : MonoBehaviour
{
    [SerializeField] private GameManagerMenu gameManagerMenu = null;

    [SerializeField] private TMP_InputField inputField = null;

    [SerializeField] private GameObject connectButtonUI = null;
    [SerializeField] private GameObject cancelConnectButtonUI = null;
    [SerializeField] private GameObject backButtonUI = null;

    [SerializeField] private GameObject ConnectMenuUI = null;
    [SerializeField] private GameObject aboutUI = null;
    //[SerializeField] private GameObject campainUI = null;
//    [SerializeField] private GameObject campainLobbyUI = null;

//    [SerializeField] private GameObject coopButtonLobbyUI = null;
    //[SerializeField] private GameObject playButtonLobbyUI = null;
//    [SerializeField] private GameObject readyButtonLobbyUI = null;
//    [SerializeField] private GameObject backButtonLobbyUI = null;
//    [SerializeField] private GameObject cancelButtonLobbyUI = null;

    [SerializeField] private EventSystem eventSystem = null;    

    public void ClientConnectGame()
    {
        if (!NetworkClient.active)
        {
            NetworkManager.singleton.networkAddress = inputField.text;

            // Client + IP
            NetworkManager.singleton.StartClient();
        }
        StartCoroutine(WaitUntilNetworkClient());
    }

    private IEnumerator WaitUntilNetworkClient()
    {
        yield return new WaitUntil(() => !NetworkClient.active || NetworkClient.isConnected);

        if (!NetworkClient.active)
        {
            cancelConnectButtonUI.SetActive(false);
            backButtonUI.SetActive(true);
            connectButtonUI.SetActive(true);

            eventSystem.SetSelectedGameObject(connectButtonUI);
        } 
        else
        {
            // Revert connect menu to it's default state
            gameManagerMenu.MainCanvasBackground.SetActive(false);
            gameManagerMenu.CampainMap.SetActive(true);

            cancelConnectButtonUI.SetActive(false);
            backButtonUI.SetActive(true);
            connectButtonUI.SetActive(true);
            ConnectMenuUI.SetActive(false);

            // Enable UI for lobby            
//            campainUI.SetActive(true);
//            campainLobbyUI.SetActive(true);
//            readyButtonLobbyUI.SetActive(true);
//            cancelButtonLobbyUI.SetActive(true);

            // Don't needed this
//            coopButtonLobbyUI.SetActive(false);
//            playButtonLobbyUI.SetActive(false);
//            backButtonLobbyUI.SetActive(false);
            aboutUI.SetActive(false);
        }
    }

    public void StopClientConnectGame ()
    {
        StopCoroutine(WaitUntilNetworkClient());

        NetworkManager.singleton.StopClient();
    }
}
