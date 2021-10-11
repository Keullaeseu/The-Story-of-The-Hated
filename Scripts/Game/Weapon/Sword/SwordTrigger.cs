using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrigger : MonoBehaviour
{
    [SerializeField] private Sword sword = null;

    private void OnTriggerEnter(Collider other)
    {
        sword.OnSwordTriggerEnter(other);
    }
}