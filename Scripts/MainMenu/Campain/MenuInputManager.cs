using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Ke.Inputs;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuInputManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameManagerMenu gameManagerMenu = null;

    private Controls controls;
    public Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Controls();
        }
    }

    // Detect if the Cursor starts to pass over the GameObject https://docs.unity3d.com/2018.3/Documentation/ScriptReference/EventSystems.IPointerEnterHandler.html
    public void OnPointerEnter(PointerEventData eventData)
    {
        // If UI
        if (eventData.pointerEnter.gameObject.layer == 5)
        {
            gameManagerMenu.isUISelected = true;
        }
    }

    // Detect when Cursor leaves the GameObject https://docs.unity3d.com/2018.3/Documentation/ScriptReference/EventSystems.IPointerEnterHandler.html
    public void OnPointerExit(PointerEventData eventData)
    {
        // If UI
        if (eventData.pointerEnter.gameObject.layer == 5)
        {
            gameManagerMenu.isUISelected = false;
        }
    }

    private void OnEnable() => Controls.Enable();

    private void OnDisable() => Controls.Disable();
}