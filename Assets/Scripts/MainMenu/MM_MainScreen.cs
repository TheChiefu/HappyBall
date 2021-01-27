using UnityEngine;

/// <summary>
/// Manages the Main screen and its elements
/// </summary>
public class MM_MainScreen : MonoBehaviour
{
    [SerializeField] private GameObject LevelSelectCanvas;
    [SerializeField] private GameObject OptionsCanvas;
    [SerializeField] private GameObject CreditsCanvas;

    public void PressLevelSelect()
    {
        LevelSelectCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void PressOptions()
    {
        OptionsCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void PressCredits()
    {
        CreditsCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void PressExit()
    {
        Application.Quit();
    }
}
