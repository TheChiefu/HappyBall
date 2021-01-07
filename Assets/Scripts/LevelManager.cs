using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [Range(0,3)]
    public int cameraMode = 0;

    [Header("Level Settings:")]
    [SerializeField] private bool inEditor = false;
    public Transform[] respawnPoints;
    public int currentCheckpoint = 0;
    public float winDelayTime = 2f;
    public bool levelEnded = false;

    [Header("Win Requirements:")]
    public int WinScore = 0;
    public int WinSwitches = 0;

    [Header("Tracked Items:")]
    public float timeRemaining = 120;
    public int switchesActivated = 0;
    public int scoreMultiplier = 1;
    public int totalScore { get; private set; }

    [Header("Level Cosmestics")]
    public Texture PrimaryTexture;
    public Texture SecondaryTexture;
    public Texture TertiaryTexture;
    public Color PrimaryColor;
    public Color SecondaryColor;
    public Color TertiaryColor;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        UpdateTime();
    }


    private void UpdateTime()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            HUD_Manager.instance.UpdateTime(timeRemaining);
        }
    }

    /// <summary>
    /// Update score with total score plus value
    /// </summary>
    /// <param name="value"></param>
    public void UpdateScore(int value, int totalScore)
    {
        totalScore += value;
        HUD_Manager.instance.UpdateScore(totalScore);
    }

    public void EndLevel()
    {
        StartCoroutine(Wait(winDelayTime));
    }

    private IEnumerator Wait(float timer)
    {
        yield return new WaitForSeconds(timer);
        if (inEditor) UnityEditor.EditorApplication.isPlaying = false;
        levelEnded = true;
    }

    public IEnumerator ModifyMultiplier(int value, float timer)
    {
        scoreMultiplier = value;
        yield return new WaitForSeconds(timer);
        scoreMultiplier = 1;
    }
}
