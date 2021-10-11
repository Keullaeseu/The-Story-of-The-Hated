using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class UnitRanger : NetworkBehaviour
{
    [SerializeField] private Unit unit = null;

    [Header("Move")]
    [SerializeField] private bool canControll = false;
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    [SerializeField] public bool IsMove = false;

    [Header("Target")]
    [SerializeField] private Targeter targeter = null;
    [SerializeField] private float followingRange = 10f;

    [SerializeField] private GameObject arrowPrefab = null;
    [SerializeField] private Transform arrowSpawnPoint = null;
    [SerializeField] private float fireRange = 5f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float rotationSpeed = 20f;

    private float lastFireTime;

    public override void OnStartServer()
    {
        enabled = true;    
    }

    [ServerCallback]
    private void FixedUpdate()
    {
        movement();

        // Cache target unit
        Unit target = targeter.GetTargetUnit();        

        if (target == null || !CanFireAtTarget()) { return; }

        // Cache target center
        Vector3 targetUnitAimCenter = target.GetUnitTargetAim();

        // Rotate to target unit
        Quaternion targetRotation = Quaternion.LookRotation(targetUnitAimCenter - transform.position);
        // Apply rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        // If unit can shoot arrow (Need update with animation)
        if (Time.time > (1 / fireRate) + lastFireTime)
        {
            Quaternion arrowRotation = Quaternion.LookRotation(targetUnitAimCenter - arrowSpawnPoint.position);

            GameObject arrowInstance = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowRotation);

            // Set team for arrow (friendly fire issue)
            arrowInstance.GetComponent<Arrow>().SetTeam(unit.GetTeam());

            // Spawn arrow in network
            NetworkServer.Spawn(arrowInstance, connectionToClient);

            // Clear time
            lastFireTime = Time.time;
        }
    }

    [Server]
    private bool CanFireAtTarget()
    {
        return (targeter.GetTargetUnit().transform.position - transform.position).sqrMagnitude <= fireRange * fireRange;
    }

    [Server]
    private void movement()
    {
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
}