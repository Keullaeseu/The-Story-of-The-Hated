using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class UnitUI : NetworkBehaviour
{
    [Header("UI")]
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;

    #region UI

    [Client]
    public void Select()
    {
        onSelected?.Invoke();
    }

    [Client]
    public void Deselect()
    {
        onDeselected?.Invoke();
    }

    [Client]
    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    #endregion

}
