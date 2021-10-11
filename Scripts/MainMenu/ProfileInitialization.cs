using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Text;

public class ProfileInitialization : MonoBehaviour
{
    [SerializeField] private GameManagerMenu gameManagerMenu = null;

    [SerializeField] private TMP_Text profileName;

    [SerializeField] private RawImage profileGameObject;

    [SerializeField] private GameObject firstInitializationUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject aboutUI;
    [SerializeField] private GameObject profileUI;

    public static string DisplayName { get; private set; }
    public static Texture2D ProfileImage { get; private set; }

    public void StartProfileInitialization()
    {
        DirectoryCreate();
        LoadProfileNameFromJson();

        if (File.Exists("./Profile/Avatar.png"))
        {
            ProfileImage = new Texture2D(2, 2);
            StartCoroutine("load_image");
        }
    }

    IEnumerator load_image()
    {
        using UnityWebRequest uwr = UnityWebRequest.Get("file:///./Profile/Avatar.png");
        yield return uwr.SendWebRequest();
        if (string.IsNullOrEmpty(uwr.error))
        {
            ProfileImage.LoadImage(uwr.downloadHandler.data);
            profileGameObject.texture = ProfileImage;
        }
        else
        {
            Debug.Log(uwr.error);
        }
    }

    private void DirectoryCreate()
    {
        try
        {
            // Determine whether the directory exists.
            if (Directory.Exists("./Profile"))
            {
                return;
            }

            // Try to create the directory.
            Directory.CreateDirectory("./Profile");
        }
        finally { }
    }

    private void LoadProfileNameFromJson()
    {
        if (File.Exists("./Profile/Profile.json"))
        {
            UIActivate();

            string json = File.ReadAllText("./Profile/Profile.json");
            PlayerProfile data = JsonUtility.FromJson<PlayerProfile>(json);

            profileName.text = data.Name;
            DisplayName = data.Name;
        }
    }

    private void UIActivate()
    {
        firstInitializationUI.SetActive(false);
        mainMenuUI.SetActive(true);
        aboutUI.SetActive(true);
        profileUI.SetActive(true);

        gameManagerMenu.eventSystem.SetSelectedGameObject(gameManagerMenu.CampainButton);
    }
}
