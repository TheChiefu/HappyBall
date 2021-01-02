using UnityEngine;

public class SmartCam : MonoBehaviour
{
    public Transform target;

    [SerializeField] private float cameraTransitionTime = 1f;
    [SerializeField] private float distanceOffset = 3;
    [SerializeField] private float heightOffset = 1;
    [SerializeField] private Transform player;

    [Header("Clamps")]
    [SerializeField] private float ClampYRotMin = -25;
    [SerializeField] private float ClampYRotMax = 70;

    [SerializeField]
    private float currentX = 0;
    private float currentY = 0;
    private InputManager _im;
    private PlayerController _pc;
    private LevelManager _lm;

    private void Start()
    {
        player = target;
        _im = InputManager.instance;
        _pc = target.GetComponentInParent<PlayerController>();
        _lm = LevelManager.instance;
    }


    public void Update()
    {
        if (!GameManager.instance.gameIsPaused)
        {
            
        }

        if (target != null && _pc != null)
        {
            //Fix camera switching (should not be in update!)
            switch (_lm.cameraMode)
            {
                case 0:
                    //Nothing, player is free moving
                    break;
                case 1:
                    Orbit();
                    break;
                case 2:
                    SideScrollPlayer();
                    break;
                case 3:
                    FixedCamera();
                    break;
                default:
                    break;
            }
        }
    }

    private void NoTarget()
    {
        
    }

    /// <summary>
    /// Orbit around the target, rotates while looking around target depending on input
    /// </summary>
    private void Orbit()
    {
        currentX = _im.CameraRotation.x;
        currentY = _im.CameraRotation.y * _im.CameraSensitivity;
    }

    /// <summary>
    /// Follow player by sideview. Player's left and right is always the same as cameras.
    /// </summary>
    private void SideScrollPlayer()
    {
        //Follow player plus a given distance and hight offset
        transform.position = player.position + (Vector3.forward * distanceOffset) + (Vector3.up * heightOffset);

        Vector3 eulerRotation = new Vector3(0, target.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(eulerRotation);
    }

    /// <summary>
    /// Set rotation and transform of camera to target location (aka fixed)
    /// </summary>
    private void FixedCamera()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
