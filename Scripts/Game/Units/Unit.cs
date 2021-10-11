using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.AI;

public class Unit : NetworkBehaviour
{
    // Need update in separeted scripts for Unit produced\spawned\guard

    [Header("Var")]
    [SerializeField] private GameObject unitPreview = null;
    [SerializeField] private Sprite avatar = null;
    [SerializeField] private int id = -1;
    [SerializeField] private int price = 100;
    [SyncVar]
    [SerializeField] private int team = -1;
    //[SerializeField] private float followingRange = 10f;
    [SerializeField] private bool isProductionUnit = false;

    [Header("Who can move")]
    // Bool for check is unit can move?
    [SerializeField] private bool canMove = false;
    [SerializeField] private bool canControll = false;
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    //[SerializeField] private Animator animator;

    [SerializeField] private Targeter targeter = null;
    [SerializeField] private GameObject unitTargetAim = null;
    [SerializeField] private bool isTargetable = true;

    [SerializeField] public bool IsMove = false;

    [Header("Selected by")]
    [SerializeField] private UnitSelection playerUnitSelection = null;

    // Need update events due to standart stop client\ stop host - mirror
    public static event Action<Unit> ServerOnUnitSpawned;
    public static event Action<Unit> ServerOnUnitDespawned;

    public static event Action<Unit> AuthorityOnUnitSpawned;
    public static event Action<Unit> AuthorityOnUnitDespawned;

    /*
        [ServerCallback]
        private void FixedUpdate()
        {
            // If unit can't walk return
            if (navMeshAgent == null) { return; }

            // Following target if unit can controlled
            if (canControll && targeter.GetTargetUnit() != null)
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

            // For moving
            if (!navMeshAgent.hasPath || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance) { return; }

            // Stop moving if at place
            navMeshAgent.ResetPath();
            IsMove = false;
        }
        */
    #region Get

    public GameObject GetUnitPreview()
    {
        return unitPreview;
    }

    public Sprite GetAvatar()
    {
        return avatar;
    }

    public int GetId()
    {
        return id;
    }

    public int GetPrice()
    {
        return price;
    }

    public int GetTeam()
    {
        return team;
    }

    public bool GetIsProductionUnit()
    {
        return isProductionUnit;
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public bool GetCanCotrolled()
    {
        return canControll;
    }

    public bool GetIsTargetable()
    {
        return isTargetable;
    }

    public Vector3 GetUnitTargetAim()
    {
        return unitTargetAim.transform.position;
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return navMeshAgent;
    }

    public Targeter GetTargeter()
    {
        return targeter;
    }

    public UnitSelection GetPlayerUnitSelection()
    {
        return playerUnitSelection;
    }

    #endregion

    #region Set

    public void SetTeam(int value)
    {
        team = value;
    }

    public void SetPlayerUnitSelection(UnitSelection value)
    {
        playerUnitSelection = value; 
    }

    #endregion

    #region Server

    public override void OnStartServer()
    {
        ServerOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnUnitDespawned?.Invoke(this);
    }

    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        AuthorityOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopClient()
    {
        if (!hasAuthority) { return; }

        AuthorityOnUnitDespawned?.Invoke(this);
    }

    #endregion

}