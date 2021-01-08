using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    /// <summary>
    /// Index Order:
    /// 0 - Speed
    /// 1 - Light Weight
    /// 2 - Heavy Weight
    /// 3 - Invincible
    /// 4 - Multiplier
    /// 5 - Add Health
    /// </summary>
    [SerializeField] private MultilanguageSO[] ML_EffectName = null;

    //Static instance check
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    //Display Inital Values
    private void Start()
    {
        _lm = LevelManager.instance;
        langIndex = GameManager.instance.languageIndex;
        UpdateMultiplier(_lm.scoreMultiplier);
        UpdateScore(_lm.totalScore);
    }


    /// <summary>
    /// Update time display with given seconds converted to TimeSpan
    /// </summary>
    /// <param name="timeRemaining"></param>
    public void UpdateTime(ref float timeRemaining)
    {
        var span = new System.TimeSpan(0, 0, (int)timeRemaining);
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

    /// <summary>
    /// Update multiplier with value
    /// </summary>
    /// <param name="value"></param>
    public void UpdateMultiplier(int value)
    {
        Multiplier.text = string.Format("{0} (x{1})", ML_Multiplier.GetText(langIndex), value);
    }
}
