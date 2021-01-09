using UnityEngine;
using Cinemachine;

/// <summary>
/// Class that enables a new Cinemachine camera on Player collision with trigger
/// </summary>
public class CameraTrigger : MonoBehaviour
{

    //Higher prority camera to main, that the Cinemachine brain switch to while active
    public new CinemachineVirtualCamera camera;


    //Have this camera track the player object on collision
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

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
