using UnityEngine;

/// <summary>
/// Deals with tagged physics objects. For use in handling their data.
/// </summary>
public class PhysicsObject : MonoBehaviour
{
    [HideInInspector] public Vector3 respawnPosition;
    [HideInInspector] public Quaternion respawnRotation;

    private void Awake()
    {
        respawnPosition = transform.position;
        respawnRotation = transform.rotation;
    }
}
