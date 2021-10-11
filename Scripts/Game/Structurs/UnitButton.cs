using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitButton : MonoBehaviour
{
    [SerializeField] private NetworkPlayerGame networkPlayerGame = null;
    [SerializeField] private Transform spawnPoint = null;

    // On button click
    public void spawnUnit(int unitId)
    {
        // If Paladin or Archer
        if (unitId == 1 || unitId == 2)
        {
            // If team have barracks
            if (networkPlayerGame.BarracksList.Count == 0) { return; }

            // Spawn unit in barracks
            spawnInBarracks(unitId);
        }
    }

    private void spawnInBarracks(int unitId)
    {
        for (int i = 0; i < networkPlayerGame.BarracksList.Count; i++)
        {
            // Cache
            StructureUnitProduction unitProduction = networkPlayerGame.BarracksList[i].GetComponent<StructureUnitProduction>();

            // If in selected barracks not limit unit
            if (unitProduction.GetUnitSpawnedCount() < unitProduction.GetMaxUnitSpawned())
            {
                // Set spawn point from free barracks
                spawnPoint = unitProduction.GetSpawnPoint();
                // Spawn unit
                networkPlayerGame.CmdSpawnProductionUnit(unitId, spawnPoint.transform.position, networkPlayerGame.GetTeam(), networkPlayerGame.BarracksList[i]);

                // After found free space
                return;
            }
        }

        Debug.Log("No free space in barracks");
    }
}