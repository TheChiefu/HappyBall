using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player Data")]
    public UserData userData = new UserData();

    [Header("Other")]
    public int languageIndex = 0;   //0 - English / 1 - Japanese
    private string applicationPath = string.Empty;
    public bool gameIsPaused;

    [Header("UI Elements")]
    [SerializeField] private GameObject pauseScreen;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

            applicationPath = Utility.GetUserSavePath();
            userData.Load(Utility.GetUserSavePath());
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        InputManager.instance.ChangeToMenu();
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
}
