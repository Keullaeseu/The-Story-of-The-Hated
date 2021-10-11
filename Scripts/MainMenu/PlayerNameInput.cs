using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private TMP_Text profileName;

    [SerializeField] private GameObject firstInitializationUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject aboutUI;
    [SerializeField] private GameObject profileUI;

    public void SaveProfileNameToJson()
    {
        PlayerProfile data = new PlayerProfile();

        data.Name = profileName.text;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText("./Profile/Profile.json", json);

        firstInitializationUI.SetActive(false);
        mainMenuUI.SetActive(true);
        aboutUI.SetActive(true);
        profileUI.SetActive(true);
    }
}
