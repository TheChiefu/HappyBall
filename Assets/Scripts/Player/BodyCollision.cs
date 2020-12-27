using UnityEngine;

/// <summary>
/// Attached script to ball body to send over collision data to parent
/// </summary>
public class BodyCollision : MonoBehaviour
{
    [SerializeField] private PlayerController _pc;

    private void Start()
    {
        if (_pc == null) _pc.GetComponentInParent<PlayerController>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") _pc.isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") _pc.isGrounded = false;
    }
}
