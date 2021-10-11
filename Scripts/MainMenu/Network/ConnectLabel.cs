using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class ConnectLabel : MonoBehaviour
{

    void Start()
    {
        gameObject.GetComponent<TMP_InputField>().text = NetworkManager.singleton.networkAddress + "";
    }
}