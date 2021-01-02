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

        _ac = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!_pc.isGrounded)
        {
            _ac.clip = hitSounds[Random.Range(0, hitSounds.Length)];
            _ac.Play();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _pc.isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") _pc.isGrounded = false;
    }
}
