using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UnitSpawnOnStart : NetworkBehaviour
{
    // Structure whom belongs this unit
    [SerializeField] private UnitSpawnOnStartControl unitSpawnedOnStartControl = null;

    public void SetUnitSpawnedOnStartControl(UnitSpawnOnStartControl value)
    {
        unitSpawnedOnStartControl = value;
    }

    [Server]
    public GameObject GetUnitSpawnedOnStartControlGameObject()
    {
        return unitSpawnedOnStartControl.gameObject;
    }
}