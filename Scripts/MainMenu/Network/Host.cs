using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Host : MonoBehaviour
{

    public void HostCooperativeGame()
    {
        if (!NetworkClient.active)
        {
            // Server + Client
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                NetworkManager.singleton.StartHost();
            }
        }
    }
}
