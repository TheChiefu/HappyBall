using UnityEngine;

/// <summary>
/// Deals with tagged physics objects. For use in handling their data.
/// </summary>
public class PhysicsObject : MonoBehaviour
{
    [HideInInspector] public Vector3 respawnPosition;
    [HideInInspector] public Quaternion respawnRotation;
    private Rigidbody _rb;

    private float initalMass;



    private void Awake()
    {
        respawnPosition = transform.position;
        respawnRotation = transform.rotation;

        if(_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
            initalMass = _rb.mass;
        }
    }


    /// <summary>
    /// Perminately modify mass of this object (therefore gravity effects)
    /// </summary>
    /// <param name="amount"></param>
    public void ModifyGravity(float amount)
    {
        _rb.mass = amount;
    }

    /// <summary>
    /// Reset ball back to inital mass amount
    /// </summary>
    public void ResetGravity()
    {
        _rb.mass = initalMass;
    }
}
