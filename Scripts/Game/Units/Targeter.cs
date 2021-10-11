using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Targeter : NetworkBehaviour
{
    [SerializeField] private Unit localUnit = null;
    [SerializeField] private Unit target = null;

    [Server]
    public void SetTarget(GameObject targetGameObject)
    {
        if (!targetGameObject.TryGetComponent<Unit>(out Unit unit)) { return; }
        // If can't be a target and our team return
        if (!unit.GetIsTargetable() || unit.GetTeam() == localUnit.GetTeam()) { return; }

        target = unit;
    }

    [Server]
    public void ClearTarget()
    {
        target = null;
    }

    [Server]
    public Unit GetTargetUnit()
    {
        return target;
    }
}