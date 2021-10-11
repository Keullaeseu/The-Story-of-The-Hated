using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Sword : NetworkBehaviour
{
    [SerializeField] private Unit unit = null;    

    [SerializeField] private int damage = 10;

    [SerializeField] private LayerMask collideLayer = new LayerMask();

    [ServerCallback]
    public void OnSwordTriggerEnter(Collider other)
    {
        // If collide layer return
        if ((collideLayer.value & (1 << other.gameObject.layer)) > 0) { return; }

        // If hit Unit component
        if (other.TryGetComponent<Unit>(out Unit unitHit))
        {
            // Return if it's from our team
            if (unitHit.GetTeam() == unit.GetTeam()) { return; }
        }

        if (other.TryGetComponent<UnitHealth>(out UnitHealth unitHealth))
        {
            // Do damage to enemy
            unitHealth.DoDamage(damage);
        }
    }
}