using UnityEngine;

/// <summary>
/// Gameobject that sends rigidbody in direction given
/// </summary>
public class GravityLift : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float multiplier = 1f;
    private ParticleSystem particles;


    //Set particle path to match direction
    private void Awake()
    {
        if (particles == null)
        {
            particles = GetComponentInChildren<ParticleSystem>();
            var vel = particles.velocityOverLifetime;
            vel.xMultiplier = direction.x;
            vel.yMultiplier = direction.y;
            vel.zMultiplier = direction.z;
        }
    }

    //Send rigidbody towards given direction
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rg = other.GetComponent<Rigidbody>();
        if(rg != null)
        {
            //Set velocity of object for consistent movement and direction
            rg.velocity = direction * multiplier;
        }
    }


    // Unity Editor //

    private void OnDrawGizmos()
    {
        //Lift direction
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + direction);

        //Calculated path
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, this.transform.position + EstimatePath(direction));

    }

    /// <summary>
    /// Calculate trajectory pased on angle and default 25 mass of player
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    private Vector3 EstimatePath (Vector3 angle)
    {
        return Vector3.zero;
    }
}
