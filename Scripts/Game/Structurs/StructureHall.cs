using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class StructureHall : NetworkBehaviour
{
    [SerializeField] private Unit unit = null;
    [SerializeField] private List<StructureHouse> structureHouseList = new List<StructureHouse>();
    [SerializeField] private List<StructureHouse> structureHouseWaitingMoneyCollector = new List<StructureHouse>();
    [SerializeField] private UnitSpawnOnStartControl unitSpawnOnStartControl = null;
    [SerializeField] private GameObject storeMoneyPoint = null;

    [SerializeField] private GameObject player = null;

    [Header("Game")]
    [SyncVar(hook = nameof(MoneyUIUpdate))]
    private int money = 0;

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
                singlePlayerNetworkPlayer.SetPlayerHall(this.gameObject);

                // Link hall storage to money UI player
                player.GetComponent<NetworkPlayerGame>().SetMoney(money);

                break;
            }
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Hall was destroyed.");
    }

    #region Get

    [Server]
    public int GetStructureHouseListCount()
    {
        return structureHouseList.Count;
    }

    [Server]
    public List<StructureHouse> GetStructureHouseList()
    {
        return structureHouseList;
    }

    [Server]
    public int GetStructureHouseWaitingMoneyCollectorCount()
    {
        return structureHouseWaitingMoneyCollector.Count;
    }

    [Server]
    public StructureHouse GetStructureHouseWaitingMoneyCollector()
    {
        return structureHouseWaitingMoneyCollector[0];
    }

    [Server]
    public UnitSpawnOnStartControl GetUnitSpawnOnStartControl()
    {
        return unitSpawnOnStartControl;
    }

    [Server]
    public GameObject GetStoreMoneyPoint()
    {
        return storeMoneyPoint;
    }

    #endregion

    #region Set\Add\Remove

    [Server]
    public void AddStructureHouseList(StructureHouse value)
    {
        structureHouseList.Add(value);
    }

    [Server]
    public void RemoveStructureHouseList(StructureHouse value)
    {
        structureHouseList.Remove(value);
    }

    [Server]
    public void AddStructureHouseWaitingMoneyCollector(StructureHouse value)
    {
        structureHouseWaitingMoneyCollector.Add(value);
    }

    [Server]
    public void RemoveStructureHouseWaitingMoneyCollector(StructureHouse value)
    {
        structureHouseWaitingMoneyCollector.Remove(value);
    }

    [Server]
    public void RemoveStructureHouseWaitingMoneyCollectorZeroIndex()
    {
        structureHouseWaitingMoneyCollector.RemoveAt(0);
    }

    [Server]
    public void SetMoney(int value)
    {
        money += value;
    }

    #endregion

    #region Void

    private void MoneyUIUpdate(int oldValue, int newValue)
    {
        player.GetComponent<NetworkPlayerGame>().SetMoney(newValue);
    }

    #endregion

}