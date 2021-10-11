using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ClientDisconnect : MonoBehaviour
{
    [SerializeField] private GameManagerMenu gameManagerMenu = null;

    public void ClientDisconnectGame ()
    {
        if (NetworkClient.active)
        {
            gameManagerMenu.MainCanvasBackground.SetActive(true);
            gameManagerMenu.CampainMap.SetActive(false);
            NetworkManager.singleton.StopClient();
        }
    }
}
