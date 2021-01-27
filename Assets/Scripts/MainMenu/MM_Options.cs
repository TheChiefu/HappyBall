using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Manages the Options screen and its elements
/// </summary>
public class MM_Options : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject CharacterCustomizationCanvas;
    [SerializeField] private GameObject SettingsCanvas;

    [Header("Options Fields")]
    [SerializeField] private TMP_Dropdown ResolutionDropdown;
    [SerializeField] private Toggle Fullscreen_Toggle;
    [SerializeField] private TMP_Dropdown Fullscreen_Mode;
    [SerializeField] private TMP_InputField FPS_Cap;
    [SerializeField] private TMP_Dropdown TextureQuality;
    [SerializeField] private TMP_Dropdown Anisotropic;
    [SerializeField] private TMP_Dropdown AntiAlias;
    [SerializeField] private TMP_Dropdown ShadowResolutionDropdown;
    [SerializeField] private TMP_InputField ShadowDistance;
    [SerializeField] private TMP_Dropdown Vsync;
    [SerializeField] private TMP_Dropdown LanguageIndex;

    [Header("Player Customization Fields")]
    [SerializeField] private TMP_InputField Username;
    [SerializeField] MeshRenderer Ball;

    private PreferencesData pd;
    private UserData ud;
    private Resolution[] resolutions;

    private void Awake()
    {
         resolutions = Screen.resolutions;
    }

    private void Start()
    {
        pd = GameManager.instance.saveData.pd;
        ud = GameManager.instance.saveData.ud;
        SetButtonValues();
        SetPlayerCustomization();
    }

    public void SetButtonValues()
    {
        //Add all available resolutions to Resolution Dropdown
        ResolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
        }
        ResolutionDropdown.AddOptions(options);

        //Set rest of values
        ResolutionDropdown.value = resolutions.Length; //Fix
        Fullscreen_Toggle.isOn = Screen.fullScreen;
        Fullscreen_Mode.value = (int)Screen.fullScreenMode;
        FPS_Cap.text = Application.targetFrameRate.ToString();
        TextureQuality.value = QualitySettings.masterTextureLimit;
        Anisotropic.value = (int)QualitySettings.anisotropicFiltering;
        AntiAlias.value = QualitySettings.antiAliasing;
        ShadowResolutionDropdown.value = (int)QualitySettings.shadowResolution;
        ShadowDistance.text = QualitySettings.shadowDistance.ToString();
        Vsync.value = QualitySettings.vSyncCount;
        LanguageIndex.value = GameManager.instance.languageIndex;
    }

    public void SetPlayerCustomization()
    {
        Username.text = ud.username;
        Ball.material.color = ud.ballColor;
    }


    // Button Presses // 

    public void PressMainBack()
    {
        MainMenuCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void SaveOptions()
    {
        //Overwrite old preference data
        pd.isFullscreen = Fullscreen_Toggle.isOn;
        pd.resolutionIndex = ResolutionDropdown.value;
        pd.fullscreenModeIndex = Fullscreen_Mode.value;
        pd.fpsCap = int.Parse(FPS_Cap.text);
        pd.textureQualityIndex = TextureQuality.value;
        pd.anisotropicIndex = Anisotropic.value;
        pd.antiAliasIndex = AntiAlias.value;
        pd.shadowResIndex = ShadowResolutionDropdown.value;
        pd.shadowDistance = int.Parse(ShadowDistance.text);
        pd.vsyncIndex = Vsync.value;
        pd.langIndex = LanguageIndex.value;

        //Set application settings
        Utility.SetGameSettings(pd);

        //Save to file
        GameManager.instance.Save();
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
        ud.username = Username.text;
        ud.ballColor = Ball.material.color;

        GameManager.instance.Save();
    }
}
