using UnityEngine;

public class SmartCam : MonoBehaviour
{
    [Header("Camera Properties:")]
    [SerializeField] private float cameraTransitionTime = 1f;
    [SerializeField] private Transform player;
    [SerializeField] private Cinemachine.CinemachineFreeLook freeLookCam;

    private InputManager _im;
    private PlayerController _pc;
    private LevelManager _lm;
    private Cinemachine.CinemachineBrain _cb;
    private bool flippingCamera = false;

    private void Start()
    {
        if (player == null) Debug.LogError("No player transform connected to: " + this.name);
        if (_im == null)    _im = InputManager.instance;
        if (_pc == null)    _pc = player.GetComponentInParent<PlayerController>();
        if (_lm == null)    _lm = LevelManager.instance;
        if (_cb == null) _cb = GetComponent<Cinemachine.CinemachineBrain>();
        if (_cb != null) _cb.m_DefaultBlend.m_Time = cameraTransitionTime;
    }

    /// <summary>
    /// Perform operations in fixed update for so it can be paused
    /// </summary>
    public void FixedUpdate()
    {
        if (player != null && _pc != null)
        {
            if (_pc.GetMovementType() == 1) FlipCamera(180);
        }
    }

    /// <summary>
    /// Flip camera angle by value
    /// </summary>
    private void FlipCamera(int value)
    {
        if (freeLookCam != null)
        {
            if (_im.flipCamera && flippingCamera == false)
            {
                flippingCamera = true;
                freeLookCam.m_XAxis.Value = value;
            }

            if (!_im.flipCamera)
            {
                flippingCamera = false;
            }
        }
    }
}
