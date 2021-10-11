using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Arrow : NetworkBehaviour
{
    [SerializeField] private Rigidbody rigidBody = null;
    private int team = -1;

    [SerializeField] private float destroyAfterSeconds = 5f;
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private int damage = 10;

    // Layer wich collide
    [SerializeField] private LayerMask collideLayer = new LayerMask();

    public override void OnStartServer()
    {
        // Destroy after time
        Invoke(nameof(Destroy), destroyAfterSeconds);

        // Add force
        rigidBody.velocity = transform.forward * launchForce;
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        // If collide layer return
        if ((collideLayer.value & (1 << other.gameObject.layer)) > 0) { return; }

        // If hit Unit component
        if (other.TryGetComponent<Unit>(out Unit unit))
        {
            // Return if it's from our team
            if (unit.GetTeam() == team) { return; }
        }

        if (other.TryGetComponent<UnitHealth>(out UnitHealth unitHealth))
        {
            // Do damage to enemy
            unitHealth.DoDamage(damage);
        }

        // After hit attach to collided object
        gameObject.transform.parent = other.gameObject.transform;
        // Stop forcing arrow
        rigidBody.velocity = Vector3.zero;
    }

    #region Set

    public void SetTeam(int value)
    {
        team = value;
    }

    #endregion

    [Server]
    private void Destroy()
    {
        NetworkServer.Destroy(gameObject);
    }
}