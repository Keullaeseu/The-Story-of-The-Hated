using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
/*
public class JsonReadWriteSystem : MonoBehaviour
{

    public void SaveProfileNameToJson()
    {
        string json = JsonUtility.ToJson(gameObject.GetComponent<Initialization>().ProfileName.text, true);
        File.WriteAllText("./Profile/Profile.json", json);
    }

    public void LoadProfileNameFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/Profile/Profile.json");
        ProfileName data = JsonUtility.FromJson<ProfileName>(json);

        gameObject.GetComponent<Initialization>().ProfileName.text = data.Name;
    }
}
*/