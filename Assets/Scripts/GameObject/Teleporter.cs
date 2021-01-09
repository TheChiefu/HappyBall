using UnityEngine;

/// <summary>
/// Simple teleporter that brings objects from point A to B
/// </summary>
public class Teleporter : MonoBehaviour
{
    [SerializeField] Transform endLocation;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = endLocation.position;
    }

    //Draw line in editor towards teleport end location
    private void OnDrawGizmosSelected()
    {
        if(endLocation != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, endLocation.position);
        }
    }
}
