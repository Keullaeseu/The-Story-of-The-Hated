using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class PlayerAvatar : NetworkBehaviour
{
    [Header("Move")]
    //[SerializeField] private bool canMove = false;
    [SerializeField] private bool canControll = false;
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    [SerializeField] public bool IsMove = false;

    [Header("Target")]
    [SerializeField] private Targeter targeter = null;
    [SerializeField] private float followingRange = 10f;

    [ServerCallback]
    private void FixedUpdate()
    {
        movement();
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