using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Header("Player Data")]
    public UserData userData = new UserData();
    [SerializeField]
    private GameObject userDataInputPanel = null;

    [Header("Other")]
    public int languageIndex = 0;   //0 - English / 1 - Japanese
    private string applicationPath = string.Empty;
    public Camera mainCamera = null;
    public bool gameIsPaused;

    [Header("UI Elements")]
    [SerializeField] private GameObject pauseScreen;


    private void Awake()
    {
        Initialize();
        mainCamera = Camera.main;
        applicationPath = Application.persistentDataPath;
        userData.Load(Utility.GetUserSavePath());
    }

    public void UnpauseGame()
    {
        InputManager.instance.pp.SwitchCurrentActionMap("Gameplay");
        InputManager.instance.Debug_GetCurrentActionMap();

        gameIsPaused = false;
        pauseScreen.SetActive(false);
        //Time.timeScale = 1;
        Cursor_LockNHide();
    }

    public void PauseGame()
    {
        //Set InputManager mode to Menu Mode
        InputManager.instance.pp.SwitchCurrentActionMap("Menu");
        InputManager.instance.Debug_GetCurrentActionMap();

        gameIsPaused = true;
        pauseScreen.SetActive(true);
        Cursor_UnlockNShow();


        //Time.timeScale = 0;
    }

    private void Cursor_LockNHide()
    {
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Cursor_UnlockNShow()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    /// <summary>
    /// Check for other instances of GameManager script
    /// </summary>
    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogWarning("Instance of GameManager already exists, destroying new one.");
            Destroy(this);
        }
    }
}
