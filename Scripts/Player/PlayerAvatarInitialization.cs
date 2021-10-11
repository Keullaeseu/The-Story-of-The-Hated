using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerAvatarInitialization : NetworkBehaviour
{
    [SerializeField] private RagdollTest ragdollTest = null;

    public override void OnStartAuthority()
    {
        enabled = true;

        ragdollTest.enabled = true;
    }
}
