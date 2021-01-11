using UnityEngine;

/// <summary>
/// Manages the Options screen and its elements
/// </summary>
public class MM_Options : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject CharacterCustomizationCanvas;
    [SerializeField] private GameObject SettingsCanvas;

    public void PressMainBack()
    {
        MainMenuCanvas.SetActive(true);
        this.gameObject.SetActive(false);
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
