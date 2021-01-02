using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manager class that takes input from multiple input types and assigns their values to public variables
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public InputActionAsset pi;
    public PlayerInput pp = new PlayerInput();

    [Header("Menus")]
    public Vector2 Move = Vector2.zero;
    public bool Submit = false;
    public bool Cancel = false;
    public bool Pause = false;

    [Header("Gameplay")]
    public bool Jump = false;
    public Vector2 Movement = Vector2.zero;
    public Vector2 CameraRotation = Vector2.zero;
    public float CameraZoom = 0;
    public float CameraSensitivity = 0.5f;
    public bool Interact = false;

    //Menu Actions
    private InputAction IA_Move;
    private InputAction IA_Submit;
    private InputAction IA_Cancel;
    private InputAction IA_Pause;

    //Gameplay Actions
    private InputAction IA_Jump;
    private InputAction IA_Movement;
    private InputAction IA_CameraRotation;
    private InputAction IA_CameraZoom;
    private InputAction IA_Interact;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        SetupMenu();
        SetupGameplay();

    }

    private void Update()
    {
        //NONO
        //if (Pause) GameManager.instance.PauseGame();
    }

    public void Debug_GetCurrentActionMap()
    {
        Debug.Log(pp.currentActionMap.name);
    }

    private void SetupMenu()
    {
        InputActionMap menu = pi.FindActionMap("Menu");

        //2D Vectors
        IA_Move = menu.FindAction("Move");
        IA_Move.performed += ctx => Move = ctx.ReadValue<Vector2>();

        //Toggle Input
        IA_Pause = menu.FindAction("Pause");
        IA_Pause.performed += ctx => Pause = !Pause;

        //Hold Down Input
        IA_Submit = menu.FindAction("Submit");
        IA_Submit.performed += ctx => Submit = true;
        IA_Submit.canceled += ctx => Submit = false;

        IA_Cancel = menu.FindAction("Cancel");
        IA_Cancel.performed += ctx => Cancel = true;
        IA_Cancel.canceled += ctx => Cancel = false;
    }

    private void SetupGameplay()
    {
        InputActionMap gameplay = pi.FindActionMap("Gameplay");

        //2D Vectors
        IA_Movement = gameplay.FindAction("Movement");
        IA_Movement.performed += ctx => Movement = ctx.ReadValue<Vector2>();

        IA_CameraRotation = gameplay.FindAction("CameraRotation");
        IA_CameraRotation.performed += ctx => CameraRotation = ctx.ReadValue<Vector2>();


        //Floats
        IA_CameraZoom = gameplay.FindAction("CameraZoom");
        IA_CameraZoom.performed += ctx => CameraZoom = ctx.ReadValue<float>();

        //Toggle Input


        //Hold Down Input
        IA_Jump = gameplay.FindAction("Jump");
        IA_Jump.performed += ctx => Jump = true;
        IA_Jump.canceled += ctx => Jump = false;

        IA_Interact = gameplay.FindAction("Interact");
        IA_Interact.performed += ctx => Interact = true;
        IA_Interact.canceled += ctx => Interact = false;

    }

    private void OnEnable()
    {
        pi.Enable();
    }

    private void OnDisable()
    {
        pi.Disable();
    }
}
