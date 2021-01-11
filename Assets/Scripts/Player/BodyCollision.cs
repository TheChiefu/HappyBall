using UnityEngine;

/// <summary>
/// Attached script to ball body to send over collision data to parent
/// </summary>
public class BodyCollision : MonoBehaviour
{
    [SerializeField] private PlayerController _pc;
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private AudioSource _ac;

    private void Awake()
    {
        if (_pc == null) GetComponentInParent<PlayerController>();
        if (_ac == null) _ac = GetComponent<AudioSource>();
    }

    /// <summary>
    /// When player body hits an object while not grounded make a noise
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(!_pc.IsGrounded())
        {
            _ac.clip = hitSounds[Random.Range(0, hitSounds.Length)];
            _ac.Play();
        }
    }

    /// <summary>
    /// When actively colliding with ground mark player as "grounded"
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _pc.IsGrounded(true);
        }
    }

    /// <summary>
    /// When player leaves ground mark player as "not grounded"
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") _pc.IsGrounded(false);
    }
}
