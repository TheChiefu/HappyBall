using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_Manager : MonoBehaviour
{
    public static HUD_Manager instance;
    private LevelManager _lm;
    private int langIndex;

    [Header("Text Elements")]
    [SerializeField] private TextMeshProUGUI TimeRemaining = null;
    [SerializeField] private TextMeshProUGUI TotalScore = null;
    [SerializeField] private TextMeshProUGUI Multiplier = null;

    [Header("Multilanguage Objects")]
    [SerializeField] private MultilanguageSO ML_TimeRemaning = null;
    [SerializeField] private MultilanguageSO ML_TotalScore = null;
    [SerializeField] private MultilanguageSO ML_Multiplier = null;

    private void Awake()
    {
        if (instance != null) instance = this;
        else  Destroy(this);
    }

    private void Start()
    {
        langIndex = GameManager.instance.languageIndex;
        UpdateMultiplier(_lm.scoreMultiplier);
        UpdateScore(_lm.totalScore);
        UpdateTime(_lm.timeRemaining);
    }


    public void UpdateTime(float timeRemaining)
    {
        var span = new System.TimeSpan(0, 0, (int)timeRemaining);

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        TimeRemaining.text = string.Format("{0}: {1}", ML_TimeRemaning.GetText(langIndex), span);
    }

    /// <summary>
    /// Update score with total score plus value
    /// </summary>
    /// <param name="value"></param>
    public void UpdateScore(int totalScore)
    {
        TotalScore.text = string.Format("{0}: {1}", ML_TotalScore.GetText(langIndex), totalScore);
    }

    public void UpdateMultiplier(int value)
    {
        TotalScore.text = string.Format("{0} (x{1})", ML_Multiplier.GetText(langIndex), value);
    }
}
