using TMPro;
using UnityEngine;

public class About : MonoBehaviour
{
    [SerializeField] TMP_Text companyNameText = null;
    [SerializeField] TMP_Text versionText = null;

    void Start()
    {
        versionText.text = "Version " + Application.version.ToString();
        companyNameText.text = "© 2021 " + Application.companyName.ToString();
    }
}
