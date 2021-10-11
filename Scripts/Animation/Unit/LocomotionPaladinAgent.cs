using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class LocomotionPaladinAgent : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navMeshAgent;
        
    private float velocity = 0;

    [ServerCallback]
    private void FixedUpdate()
    {
        OnMove();

        // Set speed to animation from agent
        animator.SetFloat("Speed", velocity);
    }

    [Server]
    private void OnMove()
    {
        velocity = navMeshAgent.velocity.magnitude;
    }
}