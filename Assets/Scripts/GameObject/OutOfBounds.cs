using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public Transform RespawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        //Reset velocity if it has it (it should have)
        Rigidbody rg = other.gameObject.GetComponent<Rigidbody>();
        if (rg != null) rg.velocity = Vector3.zero;

        other.gameObject.transform.rotation = RespawnPoint.rotation;
        other.gameObject.transform.position = RespawnPoint.position;
    }
}
