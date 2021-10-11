using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;

public class LobbyPlayerMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] public RawImage[] playerProfileImage = new RawImage[6];
    [SerializeField] public TMP_Text[] playerNameTexts = new TMP_Text[6];
    [SerializeField] public TMP_Text[] playerReadyTexts = new TMP_Text[6];    

}
