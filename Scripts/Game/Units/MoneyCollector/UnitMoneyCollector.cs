using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class UnitMoneyCollector : NetworkBehaviour
{
    [Header("Move")]
    //[SerializeField] private bool canControll = false;
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    [SerializeField] public bool IsMove = false;

    [Header("Target")]
    [SerializeField] private Targeter targeter = null;
    [SerializeField] private float followingRange = 10f;

    // Get money from houses and take it to hall, if no houses or money hide into hall
    [SerializeField] UnitSpawnOnStart unitSpawnOnStart = null;
    [SerializeField] Unit unit = null;
    private GameObject hall = null;
    private StructureHall structureHall = null;
    private GameObject hallStoreMoneyPoint = null;

    [SerializeField] int money = 0;

    [SerializeField] private GameObject moneyCollectionPoint = null;
    [SerializeField] private StructureHouse structureHouse = null;


    [ServerCallback]
    private void Start()
    {        
        hall = unitSpawnOnStart.GetUnitSpawnedOnStartControlGameObject();
        structureHall = hall.GetComponent<StructureHall>();
    }

    [ServerCallback]
    private void FixedUpdate()
    {
        // If hall is destroyed or not goint to destination point - return
        if (hall == null || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance) { return; }

        movement();
    }

    [Server]
    private void CheckWaitingStructureForMoneyPickUp()
    {
        // If no one waiting money collector return
        if (structureHall.GetStructureHouseWaitingMoneyCollectorCount() == 0) { return; }
                
        // Set point\agent destination\structure house\walk state
        moneyCollectionPoint = structureHall.GetStructureHouseWaitingMoneyCollector().GetMoneyCollectionPoint();        
        unit.GetNavMeshAgent().SetDestination(moneyCollectionPoint.transform.position);
        structureHouse = structureHall.GetStructureHouseWaitingMoneyCollector();
        unit.IsMove = true;

        // Add unit to structure
        structureHall.GetStructureHouseWaitingMoneyCollector().SetUnitMoneyCollector(this);

        // Structure no more waiting collector
        structureHouse.SetIsInWaitingList(false);

        // Remove from waiting list
        structureHall.RemoveStructureHouseWaitingMoneyCollectorZeroIndex();
    }

    [Server]
    private void movement()
    {
        // Following target if unit can controlled
        if (unit.GetCanCotrolled() && targeter.GetTargetUnit() != null)
        {
            if ((targeter.GetTargetUnit().transform.position - transform.position).sqrMagnitude > followingRange * followingRange)
            {
                navMeshAgent.SetDestination(targeter.GetTargetUnit().transform.position);
                IsMove = true;
            }
            else if (navMeshAgent.hasPath)
            {
                navMeshAgent.ResetPath();
                IsMove = false;
            }

            return;
        }

        moneyPickup();
    }

    [Server]
    private void moneyPickup()
    {
        // Collector going to strucuture
        if (money == 0 && structureHouse == null)
        {
            CheckWaitingStructureForMoneyPickUp();

            return;
        } 
        else if (money == 0 && structureHouse != null)    // Collector near structure and need take money
        {
            // Get money from house
            money += structureHouse.GetMoney();
            structureHouse.ResetMoney();

            // Null our point
            moneyCollectionPoint = null;
            structureHouse = null;
            // Set move state
            unit.IsMove = false;

            return;
        }
        else if (money != 0 && hallStoreMoneyPoint == null)    // Collector not movign and have the money, GO TO SAFE POINT WITH MONEY DUDE (go to hall or tower to store it)
        {
            // Set point to hall
            hallStoreMoneyPoint = structureHall.GetStoreMoneyPoint();

            unit.GetNavMeshAgent().SetDestination(hallStoreMoneyPoint.transform.position);

            return;
        }
        else if (money != 0 && hallStoreMoneyPoint != null) // With money near hall store money point (Need to fix After pick money transform it to hall)
        {
            // Store into hall
            structureHall.SetMoney(money);
            // Reset money collector money
            money = 0;

            hallStoreMoneyPoint = null;

            // Before wait direct command, check waiting list
            CheckWaitingStructureForMoneyPickUp();

            return;
        }
    }
}