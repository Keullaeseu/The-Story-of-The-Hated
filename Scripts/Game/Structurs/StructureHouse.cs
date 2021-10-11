using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class StructureHouse : NetworkBehaviour
{
    [SerializeField] private Unit unit = null;
    [SerializeField] private StructureHall structureHall = null;
    [SerializeField] private GameObject moneyCollectionPoint = null;
    [SerializeField] private UnitMoneyCollector unitMoneyCollector = null;

    [SerializeField] private int money = 0;
    [SerializeField] private int moneyIncome = 110;
    [SerializeField] private float moneyIncomeTime = 10f;

    private bool isInWaitingList = false;

    [Header("Money collectors")]
    [SerializeField] private UnitSpawnOnStartControl unitSpawnOnStartControl = null;

    [ServerCallback]
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
                // (Will be error coz dev build, in prod player can't build without Hall)
                // Cache hall
                structureHall = singlePlayerNetworkPlayer.GetPlayerHall().GetComponent<StructureHall>();

                // Add this unit to list
                structureHall.AddStructureHouseList(this);

                // Get money collectors list
                unitSpawnOnStartControl = structureHall.GetUnitSpawnOnStartControl();

                break;
            }
        }

        // Set income every time
        StartCoroutine(setMoneyIncome());
    }

    [ServerCallback]
    private void OnDestroy()
    {
        // Send dude back to home when destroyed
        //unitMoneyCollector

        // It's cool to check for null, coz hall can be destroyed
        if (structureHall == null) { return; }

        structureHall.RemoveStructureHouseList(this);
    }

    #region Get

    [Server]
    public int GetMoney()
    {
        return money;
    }

    [Server]
    public GameObject GetMoneyCollectionPoint()
    {
        return moneyCollectionPoint;
    }

    #endregion

    #region Set\Reset

    [Server]
    public void SetMoneyIncome(int value)
    {
        moneyIncome = value;
    }

    [Server]
    public void SetUnitMoneyCollector(UnitMoneyCollector value)
    {
        unitMoneyCollector = value;
    }

    [Server]
    public void SetIsInWaitingList(bool value)
    {
        isInWaitingList = value;
    }

    [Server]
    public void ResetMoney()
    {
        money = 0;

        // Means collector take all the money
        unitMoneyCollector = null;
    }

    #endregion

    #region Void

    [Server]
    private IEnumerator setMoneyIncome()
    {
        yield return new WaitForSeconds(moneyIncomeTime);

        money += moneyIncome;

        if (unitMoneyCollector == null && !isInWaitingList)
        {
            // Send money pickup request
            SendRequestMoneyPickup();
        }

        // Start another one
        StartCoroutine(setMoneyIncome());
    }

    [Server]
    private void SendRequestMoneyPickup()
    {
        // Add structure to waiting, to pickup list
        if (unitMoneyCollector == null && !isInWaitingList)
        {
            // Add in list
            structureHall.AddStructureHouseWaitingMoneyCollector(this);
            // Set in waiting state
            isInWaitingList = true;
        }
    }

    #endregion

}