using UnityEngine;

/// <summary>
/// Simple script to have target always face camera
/// </summary>
public class BillboardFaceCam : MonoBehaviour
{

    [SerializeField]
    private Transform target = null;

    [SerializeField]
    private Vector3 offset = Vector3.zero;

    //Update target to always face camera
    private void Update()
    {
        transform.LookAt(GameManager.instance.mainCamera.transform);
        transform.position = target.position + offset;
    }
}
