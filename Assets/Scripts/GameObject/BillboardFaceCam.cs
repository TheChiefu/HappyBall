using UnityEngine;

/// <summary>
/// Simple script to have target always face camera
/// </summary>
public class BillboardFaceCam : MonoBehaviour
{

    [SerializeField] private Transform target = null;
    [SerializeField] private Transform mainCamera = null;
    [SerializeField] private Vector3 offset = Vector3.zero;

    //Update target to always face camera
    private void FixedUpdate()
    {
        if (target == null) mainCamera = Camera.main.transform;

        transform.LookAt(mainCamera);
        transform.position = target.position + offset;
    }
}
