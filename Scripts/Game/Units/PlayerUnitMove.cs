using Ke.Inputs;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerUnitMove : NetworkBehaviour
{
    [SerializeField] private NetworkPlayerGame networkPlayerGame = null;

    // Controls
    [SerializeField] private PlayerInitialization playerInitialization = null;
    private Controls controls = null;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private UnitSelection unitSelection = null;

    // Layer wich is floor
    [SerializeField] private LayerMask selectionLayer = new LayerMask();

    [ClientCallback]
    private void Start()
    {
        if (!isLocalPlayer) { return; }

        // Take "in game" controls
        controls = playerInitialization.Controls;

        controls.RealTimeStrategy.UnitMovement.performed += ctx => moveUnit();
    }

    public void moveUnit()
    {
        if (!isLocalPlayer || playerInitialization.isUISelected || unitSelection.SelectedUnits.Count == 0) { return; }

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, selectionLayer)) { return; }

        foreach (Unit unit in unitSelection.SelectedUnits)
        {
            // If unit can move and same team as player
            if (unit.GetCanCotrolled() && unit.GetCanMove() && unit.GetTeam() == networkPlayerGame.GetTeam())
            {
                bool isUnit = hit.collider.TryGetComponent<Unit>(out Unit hitUnit);

                // If has authority it's our avatar
                if (unit.hasAuthority)
                {
                    // If click on enemy unit, destroy, them - all
                    if (isUnit && hitUnit.GetTeam() != unit.GetTeam())
                    {
                        // If not server
                        if (!isServer)
                        {
                            // Set target to enemy
                            CmdSetTarget(unit, hitUnit.gameObject);
                        }
                        else
                        {
                            // Set target to enemy
                            unit.GetComponent<Targeter>().SetTarget(hitUnit.gameObject);
                        }                        
                    }
                    else  // Else - just move
                    {
                        CmdMove(unit, hit.point);
                    }
                }
                else if (unit.name != "PlayerAvatar(Clone)")  // If have't authority but same team and not other player avatar
                {
                    // If click on enemy unit, destroy, them - all
                    if (isUnit && hitUnit.GetTeam() != unit.GetTeam())
                    {                        
                        // Set target to enemy
                        CmdSetTarget(unit, hitUnit.gameObject);
                    }
                    else  // Else - just move
                    {
                        CmdMove(unit, hit.point);
                    }
                }
            }
        }
    }

    [Command]
    private void CmdMove(Unit unit, Vector3 position)
    {
        if (unit.GetNavMeshAgent() == null || !NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) { return; }

        // Clear target while set to move
        unit.GetTargeter().ClearTarget();

        unit.GetNavMeshAgent().SetDestination(hit.position);

        // Set move for animation
        unit.IsMove = true;
    }

    [Command]
    private void CmdSetTarget(Unit unit, GameObject target)
    {
        unit.GetComponent<Targeter>().SetTarget(target);
    }
}
