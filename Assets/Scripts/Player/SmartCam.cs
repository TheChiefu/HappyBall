using UnityEngine;

public class SmartCam : MonoBehaviour
{

    [Header("")]
    [SerializeField] private float cameraTransitionTime = 1f;
    [SerializeField] private float distanceOffset = 3;
    [SerializeField] private float heightOffset = 1;
    [SerializeField] private Transform player;

    private InputManager _im;
    private PlayerController _pc;
    private LevelManager _lm;
    private Cinemachine.CinemachineBrain _cb;

    private void Start()
    {
        if (player == null) Debug.LogError("No player transform connected to: " + this.name);
        if (_im == null)    _im = InputManager.instance;
        if (_pc == null)    _pc = player.GetComponentInParent<PlayerController>();
        if (_lm == null)    _lm = LevelManager.instance;
        if (_cb == null) _cb = GetComponent<Cinemachine.CinemachineBrain>();
        if (_cb != null) _cb.m_DefaultBlend.m_Time = cameraTransitionTime;
    }


    public void FixedUpdate()
    {
        if (player != null && _pc != null)
        {
            //Fix camera switching (should not be in update!)
            switch (_lm.cameraMode)
            {
                case 0:
                    //Nothing, player is free moving
                    break;
                case 1:
                    //Nothing
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

    /// <summary>
    /// Follow player by sideview. Player's left and right is always the same as cameras.
    /// </summary>
    private void SideScrollPlayer()
    {
        //Follow player plus a given distance and hight offset
        transform.position = player.position + (Vector3.forward * distanceOffset) + (Vector3.up * heightOffset);

        Vector3 eulerRotation = new Vector3(0, player.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(eulerRotation);
    }

    /// <summary>
    /// Set rotation and transform of camera to target location (aka fixed)
    /// </summary>
    private void FixedCamera()
    {
        transform.position = player.position;
        transform.rotation = player.rotation;
    }
}
