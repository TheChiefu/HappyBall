using UnityEngine;

/// <summary>
/// Deals with tagged physics objects. For use in handling their data.
/// </summary>
public class PhysicsObject : MonoBehaviour
{
    [HideInInspector] public Vector3 respawnPosition;
    [HideInInspector] public Quaternion respawnRotation;
    [SerializeField] private AudioClip hitSound;
    private AudioSource _as;
    private Rigidbody _rb;

    private float initalMass;



    private void Awake()
    {
        respawnPosition = transform.position;
        respawnRotation = transform.rotation;
        if (_as == null)
        {
            _as = GetComponent<AudioSource>();
            _as.clip = hitSound;
        }

        if(_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
            initalMass = _rb.mass;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_as != null && _as.clip != null) _as.Play();
    }
}
