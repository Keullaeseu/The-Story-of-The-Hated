using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class StructureUnitProduction : NetworkBehaviour
{
    [SerializeField] private Unit unit = null;
    [SerializeField] private int maxUnitSpawned = 3;
    [SerializeField] private Transform spawnPoint = null;

    [SerializeField] private GameObject player = null;

    // Spawned unit (In next update can be sync int = coun of private list, need check performance)
    private SyncList<Unit> unitSpawned = new SyncList<Unit>();
        
    private void Start()
    {
        // Find all players
        GameObject[] playersArray = GameObject.FindGameObjectsWithTag("Player");
        
        // Do for each player
        foreach (var singlePlayer in playersArray)
        {
            // Cache NetworkPlayerGame
            NetworkPlayerGame singlePlayerNetworkPlayer = singlePlayer.GetComponent<NetworkPlayerGame>();

            // If same team
            if (singlePlayer.GetComponent<NetworkIdentity>().isLocalPlayer && unit.GetTeam() == singlePlayerNetworkPlayer.GetTeam())
            {
                // Cache local player
                player = singlePlayer;

                // Add this unit to list
                singlePlayerNetworkPlayer.BarracksList.Add(this.gameObject);

                break;
            }
        }
    }

    #region Get

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

    public int GetMaxUnitSpawned()
    {
        return maxUnitSpawned;
    }

    public int GetUnitSpawnedCount()
    {
        return unitSpawned.Count;
    }

    #endregion

    [Server]
    public void AddUnitSpawned (Unit value)
    {
        unitSpawned.Add(value);
    }
}