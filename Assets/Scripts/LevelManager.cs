using System.Collections;
using UnityEngine;

/// <summary>
/// Local level manager that holds data pertaining to said level
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    /// <summary>
    /// Movement type based on index (for movement type swtching on the fly)
    /// 0 - Default: Global world movement, Forward is Z, Left/Right is X Axis, and Up and Y
    /// 1 - Target: Moves relative to target's local movement axis (3D)
    /// </summary>
    //[Range(0,3)]
    //public int cameraMode = 0;

    [Header("Level Settings:")]
    public Transform[] respawnPoints;
    public int currentCheckpoint = 0;
    public bool levelEnded = false;

    [Header("Win Requirements:")]
    public int WinScore = 0;
    public int WinSwitches = 0;

    [Header("Tracked Items:")]
    [SerializeField] private int totalScore = 0;
    [SerializeField] private float timeRemaining = 120;
    [SerializeField] private int switchesActivated = 0;
    [SerializeField] private int totalCoinsPossible = 0;

    [Header("Level Cosmestics")]
    public Texture PrimaryTexture;
    public Texture SecondaryTexture;
    public Texture TertiaryTexture;
    public Color PrimaryColor;
    public Color SecondaryColor;
    public Color TertiaryColor;

    private bool doOnce = false;

    //Static instance check
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    //Events done in pauseable fixed time update
    private void FixedUpdate()
    {
        if(!levelEnded)
        {
            UpdateTime();
        }

        //Perform level end function once in update
        if(doOnce && levelEnded)
        {
            doOnce = false;
            EndLevel();
        }
        
    }


    /// <summary>
    /// Update time HUD text and internal counter
    /// </summary>
    private void UpdateTime()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            HUD_Manager.instance.UpdateTime(ref timeRemaining);
        }
        else
        {
            doOnce = true;
            levelEnded = true;
        }
    }


    /// <summary>
    /// Perform all level ending functions
    /// </summary>
    public void EndLevel()
    {
        //Calculate star result
        int stars = 0;
        float ratio = totalScore / (float) totalCoinsPossible;

        // Calculate stars based on coin collected ratio
        // Score can be higher than coin actual due to score multiplier
        if (ratio <= 0) stars = 0;
        else if (ratio > 0 && ratio < 0.50f) stars = 1;
        else if (ratio > 0.50f && ratio < 0.75f) stars = 2;
        else if (ratio > 0.75f && ratio <= 1f) stars = 3;
        else if (ratio > 1f && ratio <= 1.25f) stars = 4;
        else stars = 5;

        //End Level and Display End of Level Screen
        levelEnded = true;
        HUD_Manager.instance.DisplayEoL(timeRemaining, totalScore, stars);

        //Auto save results
        int index = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        string name = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        LevelSaveData currentScene = new LevelSaveData(index, name, totalScore, (int)timeRemaining, stars);
        GameManager.instance.Save(currentScene);
    }


    // Getters and Setters //

    public int GetScore()
    {
        return this.totalScore;
    }

    public void AddToScore(int score)
    {
        totalScore += score;
    }

    public int GetTotalSwitches()
    {
        return this.switchesActivated;
    }

    public void AddToSwitchCounter(int count)
    {
        switchesActivated += count;
    }

    public float GetTimeRemaning()
    {
        return this.timeRemaining;
    }

    /// <summary>
    /// Adds possible total coin values in level (accessed by coins on awake)
    /// Used for calculating star value, comparing how many stars were collected by player vs total amount
    /// </summary>
    /// <param name="value"></param>
    public void AddCoins(int value)
    {
        this.totalCoinsPossible += value;
    }
}
