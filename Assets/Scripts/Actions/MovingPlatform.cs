using UnityEngine;

/// <summary>
/// Move the attached gameobject by given speed to distance, then go back to original position
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    [Header("Properties:")]
    [Tooltip("How fast platform moves")]
    [SerializeField] private float speed = 0.1f;
    [Tooltip("How long in seconds the platform waits at destination until moving again")]
    [SerializeField] private float waitTime = 1f;
    [Tooltip("Platform that is to be moved")]
    [SerializeField] private Transform platform;
    [Tooltip("Transform position in which the platform moves towards")]
    [SerializeField] private Transform destination;


    //Private calculation variables
    private Vector3 originalPostion = Vector3.zero;
    private Vector3 destinationPosition = Vector3.zero;
    private float timer = 0;
    private bool canMove = true;

    //Get inital positions
    private void Awake()
    {
        originalPostion = platform.transform.localPosition;
        destinationPosition = destination.localPosition;
    }

    //Move platform in fixed
    private void FixedUpdate()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        //Once platform reaches destination do checks
        if (platform.transform.localPosition == destinationPosition)
        {
            //If there is a wait timer
            if (waitTime > 0)
            {
                //Wait until timer hit requested wait time, in order to move
                if (timer > waitTime)
                {
                    timer = 0;
                    canMove = true;
                }
                else
                {
                    timer += Time.deltaTime;
                    canMove = false;
                }
            }

            //If swap destination and position when platform can move
            if (canMove)
            {
                Vector3 temp = originalPostion;
                originalPostion = destinationPosition;
                destinationPosition = temp;
            }
        }

        if(canMove)
        {
            //Move towards destination
            platform.transform.localPosition = Vector3.MoveTowards(platform.transform.localPosition, destinationPosition, speed);
        }
    }


    // For Unity Editor //


    private void OnDrawGizmos()
    {
        if(destination != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(platform.transform.position, destination.position);
        }
    }
}
