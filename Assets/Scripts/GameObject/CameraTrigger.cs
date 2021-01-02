using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    /// <summary>
    /// Location camera will stay at until another trigger switches positions
    /// </summary>
    public new CinemachineVirtualCamera camera;
    private int previousIndex;
    
    [Range(0,3)]
    public int cameraMode;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            camera.Follow = other.gameObject.transform;
            camera.LookAt = other.gameObject.transform;

            previousIndex = LevelManager.instance.cameraMode;
            LevelManager.instance.cameraMode = this.cameraMode;
            camera.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            camera.Follow = null;
            camera.LookAt = null;
            LevelManager.instance.cameraMode = previousIndex;
            camera.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        BoxCollider area = GetComponent<BoxCollider>();
        Gizmos.DrawCube(transform.position + area.center, area.size);
    }
}
