using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD_Manager : MonoBehaviour
{
    public static HUD_Manager instance;
    private LevelManager _lm;
    private int langIndex;

    [Header("HUD Elements")]
    [SerializeField] private GameObject HUD_Canvas = null;
    [SerializeField] private TextMeshProUGUI TimeRemaining = null;
    [SerializeField] private TextMeshProUGUI TotalScore = null;
    [SerializeField] private TextMeshProUGUI Multiplier = null;

    [Header("Multilanguage Objects")]
    [SerializeField] private MultilanguageSO ML_TimeRemaning = null;
    [SerializeField] private MultilanguageSO ML_TotalScore = null;
    [SerializeField] private MultilanguageSO ML_Multiplier = null;


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
        UpdateMultiplier(1);
        UpdateScore(0);
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
