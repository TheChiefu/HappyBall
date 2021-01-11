using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Manages the Options screen and its elements
/// </summary>
public class MM_Options : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject CharacterCustomizationCanvas;
    [SerializeField] private GameObject SettingsCanvas;

    [Header("Options Fields")]
    [SerializeField] private Toggle Fullscreen_Toggle;
    [SerializeField] private TMP_InputField FPS_Cap;

    private void Awake()
    {
        Fullscreen_Toggle.isOn = Screen.fullScreen;
        FPS_Cap.text = Application.targetFrameRate.ToString();
    }

    public void PressMainBack()
    {
        MainMenuCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void SaveOptions()
    {
        Screen.fullScreen = Fullscreen_Toggle.isOn;
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;

        Application.targetFrameRate = int.Parse(FPS_Cap.text);
    }


    //Press Character Customization Menu
    public void PressCC()
    {
        SettingsCanvas.SetActive(false);
        CharacterCustomizationCanvas.SetActive(true);
    }

    //Press back from Customizatio Menu, which brings you back to Settings menu
    public void PressCCBack()
    {
        CharacterCustomizationCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
    }

    //Save player customization
    public void PressCCSave()
    {
        GameManager.instance.Save();
    }
}
