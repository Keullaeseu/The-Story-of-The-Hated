using TMPro;
using UnityEngine;

public class TextColor : MonoBehaviour
{
    [SerializeField] Color pointerEnterColor = new Color (0, 0, 0);
    [SerializeField] Color pointerExitColor = new Color(0, 0, 0);

    public void pointerEnter()
    {
        gameObject.GetComponent<TextMeshProUGUI>().color = pointerEnterColor;
    }
    public void pointerExit()
    {
        gameObject.GetComponent<TextMeshProUGUI>().color = pointerExitColor;
    }
}
