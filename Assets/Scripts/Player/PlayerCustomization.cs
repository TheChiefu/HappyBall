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
        PlayerModel.material.color = GameManager.instance.userData.ballColor;
        PlayerNameDisplay.text = GameManager.instance.userData.username;
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
