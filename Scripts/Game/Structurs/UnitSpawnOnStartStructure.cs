using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UnitSpawnOnStartStructure : NetworkBehaviour
{
    [SerializeField] private Unit unit = null;
    [SerializeField] private UnitSpawnOnStartControl unitSpawnOnStartControl = null;
    [SerializeField] private int unitIdToSpawn = -1;
    [SerializeField] private Transform spawnPoint = null;

    [ServerCallback]
    private void Start()
    {
        // Find all players
        GameObject[] playersArray = GameObject.FindGameObjectsWithTag("Player");

        NetworkPlayerGame networkPlayerGame = null;

        // Do for each player
        foreach (var singlePlayer in playersArray)
        {
            // Find server
            if (singlePlayer.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                networkPlayerGame = singlePlayer.GetComponent<NetworkPlayerGame>();
            }
        }            

        networkPlayerGame.SpawnUnitOnStart(unitIdToSpawn, spawnPoint.position, unit.GetTeam(), unit.gameObject);
    }

    [Server]
    public void AddUnitSpawned(Unit value)
    {
        unitSpawnOnStartControl.AddUnitSpawned(value);
    }
}
