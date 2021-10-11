using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class LocomotionRangerAgent : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navMeshAgent;

    [SyncVar]
    private float velocity = 0;

    private void FixedUpdate()
    {
        OnMove();

        // Set speed to animation from agent
        animator.SetFloat("Speed", velocity);
    }

    [ServerCallback]
    private void OnMove()
    {
        velocity = navMeshAgent.velocity.magnitude;
    }
}