using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.Networking;
using TMPro;

public class Initialization : MonoBehaviour
{
    [SerializeField] ProfileInitialization profileInitialization;

    private void Awake()
    {
        profileInitialization.StartProfileInitialization();
    }
}
