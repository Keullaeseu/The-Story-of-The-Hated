using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class UnitHealth : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SyncVar(hook = nameof(HealthUIUpdate))]
    private int currentHealth;

    public event Action<int, int> ClientOnHealthUIUpdate;

    [Header("Selection")]
    [SerializeField] Unit unit = null;
    [SerializeField] private SpriteRenderer heathSpriteRenderer = null;

    public override void OnStartServer()
    {
        // Set max health on spawn unit
        currentHealth = maxHealth;
    }

    [Server]
    public void DoDamage(int damage)
    {
        if (currentHealth == 0) { return; }

        currentHealth = Mathf.Max(currentHealth - damage, 0);

        if (currentHealth != 0) { return; }

        // If we are here our hp below 0
        Die();
    }

    [Server]
    private void Die()
    {        
        NetworkServer.Destroy(gameObject);
    }

    private void deselectUnit()
    {
        // If not selected - return
        if (!heathSpriteRenderer.enabled) { return; }

        // Cache
        UnitSelection playerUnitSelection = unit.GetPlayerUnitSelection();

        // If unit index above than UI can hold it - return
        if (playerUnitSelection.SelectedUnits.IndexOf(unit) >= playerUnitSelection.GetSelectedUnitImageUICount()) { return; }

        // Check for count of selected unit list
        if (playerUnitSelection.GetSelectedUnitsCount() == 1)
        {
            playerUnitSelection.GetTargetDeactivate();
            // Remove from selected list
            playerUnitSelection.SelectedUnits.Remove(unit);

            return;
        }
        else if (playerUnitSelection.GetSelectedUnitsCount() > 1)
        {
            playerUnitSelection.GetTargetUpdate(unit);
         
            return;
        }
    }

    #region Client

    private void HealthUIUpdate(int oldHealth, int newHealth)
    {
        // If unit selected and HP - 0 or below
        if (newHealth <= 0) { deselectUnit(); }

        ClientOnHealthUIUpdate?.Invoke(newHealth, maxHealth);
    }
    #endregion

}