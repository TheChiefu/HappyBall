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

    public int totalScore { get; private set; }

    /// <summary>
    /// Time to complete level (in seconds)
    /// </summary>
    public float timeRemaining { get { return setTime; } set { setTime = value; } }
    [SerializeField] private float setTime;


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

    private void Update()
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


}
