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
    public int totalScore { get; private set; }

    [Header("Level Cosmestics")]
    public Texture PrimaryTexture;
    public Texture SecondaryTexture;
    public Texture TertiaryTexture;
    public Color PrimaryColor;
    public Color SecondaryColor;
    public Color TertiaryColor;


    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI UI_TimeRemaining = null;
    [SerializeField] private TextMeshProUGUI UI_TotalScore = null;
    private MultilanguageText UI_TimeText = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        if (UI_TimeText == null) UI_TimeText = UI_TimeRemaining.GetComponent<MultilanguageText>();
    }

    private void FixedUpdate()
    {
        UpdateTime(UI_TimeText.outputText);
    }

    /// <summary>
    /// Update time with given language text (i.e: Time Reamining or other language equvilant)
    /// </summary>
    /// <param name="text"></param>
    private void UpdateTime(string text)
    {
        var span = new System.TimeSpan(0, 0, (int)timeRemaining);

        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        UI_TimeRemaining.text = string.Format("{0}: {1}", text, span);
    }

    public void UpdateScore(int value)
    {
        totalScore += value;
        UI_TotalScore.text = string.Format("Total Score: {0}", totalScore);
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

}
