using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD_EoL : MonoBehaviour
{
    public static HUD_EoL instance;

    [Header("Visual:")]
    public Canvas DisplayCanvas = null;
    [SerializeField] private GameObject MainMenuCanvas = null;

    [Header("End of Level Elements")]
    [SerializeField] private GameObject EoL_Screen = null;
    [SerializeField] private HUD_Stars EoL_Stars = null;
    [SerializeField] private TextMeshProUGUI EoL_Time = null;
    [SerializeField] private TextMeshProUGUI EoL_Score = null;
    [SerializeField] private MultilanguageSO[] EoL_Text = null; // Multilanguage Text relating to EoL text
    private int langIndex;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        langIndex = GameManager.instance.languageIndex;
        DisplayCanvas.enabled = false;
    }

    public void DisplayEoL(float endTime, float endScore, int stars)
    {
        DisplayCanvas.enabled = true;

        HUD_Manager.instance.gameObject.SetActive(false);

        EoL_Stars.SpawnStars(stars);
        var span = new System.TimeSpan(0, 0, (int)endTime);
        EoL_Time.text = string.Format("{0}: {1}", EoL_Text[0].GetText(langIndex), span);
        EoL_Score.text = string.Format("{0}: {1}", EoL_Text[1].GetText(langIndex), endScore);

        EoL_Screen.SetActive(true);
    }

    public void BackToMenu()
    {
        GameManager.instance.GoBackToMainMenu();
        DisplayCanvas.enabled = false;
    }

    public void NextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;

        if (next >= SceneManager.sceneCountInBuildSettings)
        {
            next = 1;
            InputManager.instance.ChangeToMenu();
            MainMenuCanvas.SetActive(true);
        }

        SceneManager.LoadScene(next);
        DisplayCanvas.enabled = false;
    }
}
