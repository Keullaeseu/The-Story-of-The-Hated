using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HostCancel : MonoBehaviour
{
    public void CancelHostGame ()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
    }
}
