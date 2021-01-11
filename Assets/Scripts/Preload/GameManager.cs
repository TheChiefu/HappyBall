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
    }

    private void Update()
    {
        if (InputManager.instance.Pause)
        {
            InputManager.instance.Pause = false;
            PauseGame();
        }
    }

    public void UnpauseGame()
    {
        InputManager.instance.pp.SwitchCurrentActionMap("Gameplay");

        gameIsPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        //Set InputManager mode to Menu Mode
        InputManager.instance.pp.SwitchCurrentActionMap("Menu");

        gameIsPaused = true;
        pauseScreen.SetActive(true);

        Time.timeScale = 0;
    }

    public void GoBackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Saves all game progress to userdata instance
    /// </summary>
    public void Save()
    {
        userData.Save(applicationPath);
    }

    public void Save(LevelSaveData sd)
    {
        userData.Save(applicationPath, sd);
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

            if (mainCamera == null) mainCamera = Camera.main;
            applicationPath = Utility.GetUserSavePath();
            userData.Load(Utility.GetUserSavePath());
        }
        else
        {
            Debug.LogWarning("Instance of GameManager already exists, destroying new one.");
            Destroy(this);
        }
    }
}
