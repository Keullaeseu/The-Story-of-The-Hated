using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UnitSpawnOnStartControl : NetworkBehaviour
{
    // Spawned unit (In next update can be sync int = coun of private list, need check performance)
    private SyncList<Unit> unitSpawned = new SyncList<Unit>();

    [Server]
    public void AddUnitSpawned(Unit value)
    {
        unitSpawned.Add(value);
    }

    [Server]
    public SyncList<Unit> GetUnitSpawnedList()
    {
        return unitSpawned;
    }
}