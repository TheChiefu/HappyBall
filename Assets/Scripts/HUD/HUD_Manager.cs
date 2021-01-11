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

    [Header("End of Level Elements")]
    [SerializeField] private GameObject EoL_Screen = null;
    [SerializeField] private HUD_Stars EoL_Stars = null;
    [SerializeField] private TextMeshProUGUI EoL_Time = null;
    [SerializeField] private TextMeshProUGUI EoL_Score = null;
    [SerializeField] private MultilanguageSO[] EoL_Text = null; // Multilanguage Text relating to EoL text


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
        EoL_Screen.SetActive(false);
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

    public void DisplayEoL(float endTime, float endScore, int stars)
    {
        HUD_Canvas.SetActive(false);
        EoL_Stars.SpawnStars(stars);
        var span = new System.TimeSpan(0, 0, (int)endTime);
        EoL_Time.text = string.Format("{0}: {1}", EoL_Text[0].GetText(langIndex), span);
        EoL_Score.text = string.Format("{0}: {1}", EoL_Text[1].GetText(langIndex), endScore);

        EoL_Screen.SetActive(true);
    }



    // Button Functions //

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;

        if (next >= SceneManager.sceneCountInBuildSettings) next = 1;

        SceneManager.LoadScene(next);
    }
}
