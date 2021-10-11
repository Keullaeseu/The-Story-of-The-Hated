using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputManager : NetworkBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlayerInitialization playerInitialization = null;

    // Detect if the Cursor starts to pass over the GameObject https://docs.unity3d.com/2018.3/Documentation/ScriptReference/EventSystems.IPointerEnterHandler.html
    public void OnPointerEnter(PointerEventData eventData)
    {        
        // If UI
        if (eventData.pointerEnter.gameObject.layer == 5)
        {
            playerInitialization.isUISelected = true;
        }
    }

    // Detect when Cursor leaves the GameObject https://docs.unity3d.com/2018.3/Documentation/ScriptReference/EventSystems.IPointerEnterHandler.html
    public void OnPointerExit(PointerEventData eventData)
    {        
        // If UI
        if (eventData.pointerEnter.gameObject.layer == 5)
        {
            playerInitialization.isUISelected = false;
        }
    }

    public override void OnStartAuthority()
    {
        this.enabled = true;
    }
}