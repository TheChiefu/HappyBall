using UnityEngine;
using Cinemachine;

/// <summary>
/// Class that enables a new Cinemachine camera on Player collision with trigger
/// </summary>
public class CameraTrigger : MonoBehaviour
{

    [SerializeField]
    [Range(0,2)]
    private int MovementType = 0;

    //Higher prority camera to main, that the Cinemachine brain switch to while active
    public new CinemachineVirtualCamera camera;


    //Have this camera track the player object on collision
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //When exiting this trigger volume movement to camera specified movement
            other.gameObject.GetComponentInParent<PlayerController>().ChangeMovementType(MovementType);

            camera.Follow = other.gameObject.transform;
            camera.LookAt = other.gameObject.transform;

            camera.enabled = true;
        }
    }

    //Disable the cinemachine camera when player leaves trigger area
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //When exiting this trigger volume movement to target 3D movement
            other.gameObject.GetComponentInParent<PlayerController>().ChangeMovementType(1);

            camera.Follow = null;
            camera.LookAt = null;
            camera.enabled = false;
        }
    }


    //For unity GUI, shows trigger volume
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0.75f, 0.0f, 0.2f);
        BoxCollider area = GetComponent<BoxCollider>();
        Gizmos.DrawCube(transform.position + area.center, area.size);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        BoxCollider area = GetComponent<BoxCollider>();
        Gizmos.DrawWireCube(transform.position + area.center, area.size);
    }
}
