using UnityEngine;

/// <summary>
/// Attached to Player Prefab on top. Customizes the player object
/// </summary>
public class PlayerCustomization : MonoBehaviour
{
    [SerializeField] private MeshRenderer PlayerModel = null;
    [SerializeField] private TMPro.TextMeshProUGUI PlayerNameDisplay = null;

    private void Start()
    {
        Initialize();
        UserData ud = GameManager.instance.saveData.ud;

        PlayerModel.material.color = ud.ballColor;
        PlayerNameDisplay.text = ud.username;
    }

    /// <summary>
    /// Setup player customization such as name and model color
    /// </summary>
    private void Initialize()
    {
        if(PlayerModel == null) PlayerModel = this.GetComponentInChildren<MeshRenderer>();
        if(PlayerNameDisplay == null) PlayerNameDisplay = this.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }
}
