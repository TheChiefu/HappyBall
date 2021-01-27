using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GameObject EoL_Screen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject EventSystem;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            applicationPath = Utility.GetUserSavePath();

            //Load to main menu on first load
            SceneManager.LoadScene(1);

            Load();
        }
        else
        {
            Debug.LogWarning("Instance of GameManager already exists, destroying new one.");
            Destroy(this.gameObject);
        }

        
        
    }

    // Turn on and off the EventSystem per scene change.
    // For some reason it stops working on scene change
    // Unity Bug
    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("Here");
        StartCoroutine(TurnOnOff());
    }

    /// <summary>
    /// Did you try turning if on and off?
    /// </summary>
    /// <returns></returns>
    private IEnumerator TurnOnOff()
    {
        EventSystem.SetActive(false);
        yield return new WaitForEndOfFrame();
        EventSystem.SetActive(true);
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

        //SceneManager.UnloadScene(SceneManager.GetActiveScene());
        SceneManager.LoadScene("00_MainMenu", LoadSceneMode.Single);
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
