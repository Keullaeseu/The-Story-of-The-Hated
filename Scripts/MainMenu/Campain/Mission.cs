using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    [Scene] [SerializeField] private string missionScene = string.Empty;
    [SerializeField] private string missionName = null;
    [SerializeField] private Sprite preview = null;
    [SerializeField] private string description = null;

    public string GetMissionScene()
    {
        return missionScene;
    }

    public string GetName()
    {
        return missionName;
    }

    public Sprite GetPreview()
    {
        return preview;
    }

    public string GetDescription()
    {
        return description;
    }
}
