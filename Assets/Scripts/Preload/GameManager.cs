using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player Data")]
    [SerializeField] public SaveData saveData = new SaveData();

    [Header("Other")]
    public int languageIndex = 0;   //0 - English / 1 - Japanese
    private string applicationPath = string.Empty;
    public bool gameIsPaused;

    [Header("UI Elements")]
    [SerializeField] private GameObject MainMenuCanvases;
    [SerializeField] private GameObject MM_Main;

    [SerializeField] private GameObject pauseScreen;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            applicationPath = Utility.GetUserSavePath();
            Load();
        }
        else
        {
            Debug.LogWarning("Instance of GameManager already exists, destroying new one.");
            Destroy(this.gameObject);
        }
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
        InputManager.instance.ChangeToGameplay();

        gameIsPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        //Set InputManager mode to Menu Mode
        InputManager.instance.ChangeToMenu();

        gameIsPaused = true;
        pauseScreen.SetActive(true);

        Time.timeScale = 0;
    }

    public void GoBackToMainMenu()
    {
        pauseScreen.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1;

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        InputManager.instance.ChangeToMenu();

        MainMenuCanvases.SetActive(true);
        MM_Main.SetActive(true);
    }

    /// <summary>
    /// Saves all game progress to SaveData instance
    /// </summary>
    public void Save()
    {
        saveData.Save(applicationPath);
    }

    public void Save(UserData ud)
    {
        saveData.Save(applicationPath, ud);
    }

    public void Save(LevelSaveData sd)
    {
        saveData.Save(applicationPath, sd);
    }
    
    public void Save(PreferencesData pd)
    {
        saveData.Save(applicationPath, pd);
    }


    /// <summary>
    /// Gets all values from saved JSON file and assigns to local SaveData value
    /// </summary>
    public void Load()
    {
        if(saveData.Load(Utility.GetUserSavePath()))
        {
            Utility.SetGameSettings(saveData.pd);
        }
    }
}
