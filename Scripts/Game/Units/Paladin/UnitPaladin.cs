using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class UnitPaladin : NetworkBehaviour
{
    [Header("Move")]
    [SerializeField] private bool canControll = false;
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    //[SerializeField] public bool IsMove = false;

    [Header("Target")]
    [SerializeField] private Targeter targeter = null;
    [SerializeField] private float followingRange = 10f;

    [SerializeField] private Unit unit = null;
    [SerializeField] private GameObject swordTrigger = null;
    [SerializeField] private Animator animator = null;

    [SerializeField] private float rotationSpeed = 160f;

    public override void OnStartServer()
    {
        enabled = true;
    }

    [ServerCallback]
    private void FixedUpdate()
    {
        movement();
    }

    [Server]
    private void movement()
    {
        // Cache target unit
        Unit target = targeter.GetTargetUnit();

        // Following target if unit can controlled
        if (canControll && targeter.GetTargetUnit() != null)
        {
            // If not at target unit - move to it
            if ((target.transform.position - transform.position).sqrMagnitude > followingRange * followingRange)
            {
                navMeshAgent.SetDestination(target.transform.position);

                // Set to move
                if (!unit.IsMove) { unit.IsMove = true; }
            }
            else if (navMeshAgent.hasPath)
            {
                navMeshAgent.ResetPath();
                unit.IsMove = false;

                attack(target);
            }

            return;
        }

        // Stop attack if target dead or unit move
        if (target == null || unit.IsMove)
        {
            if (animator.GetBool("Attack"))
            {
                // Set bool for cancel attack
                animator.SetBool("Attack", false);
                // Disable sword trigger
                swordTrigger.SetActive(false);
            }

            return;
        }
    }

    [Server]
    private void attack(Unit target)
    {
        // At place with target
        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance) { return; }

        // Activate trigger for damage
        if (!swordTrigger.activeSelf) { swordTrigger.SetActive(true); }

        // Cache target center
        Vector3 targetUnitAimCenter = target.GetUnitTargetAim();

        // Rotate to target unit
        Quaternion targetRotation = Quaternion.LookRotation(targetUnitAimCenter - transform.position);
        // Apply rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        // Set attack for agent
        animator.SetBool("Attack", true);
    }
}