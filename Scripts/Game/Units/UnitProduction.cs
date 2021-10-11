using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UnitProduction : NetworkBehaviour
{
    [SerializeField] private StructureUnitProduction unitProductionStrucuture = null;

    public void SetUnitProductioStructure(StructureUnitProduction value)
    {
        unitProductionStrucuture = value;
    }
}