using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Placed on powerup visual trigger. Collects the powerup
/// </summary>
public class PowerupTrigger : MonoBehaviour
{
    Powerup PU = null;

    private void Awake()
    {
        PU = GetComponentInParent<Powerup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            if (player != null) PU.Collect(player);
        }
    }
}
