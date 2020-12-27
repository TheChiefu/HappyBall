using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_LevelSelect : MonoBehaviour
{
    [Header("Level Attributes:")]
    [SerializeField] private Sprite levelPreview;
    [SerializeField] private string[] levelDescription;
    [SerializeField] private int levelIndex;

    [Header("Effected Items:")]
    [SerializeField] private Image localLevelPreview;
    [SerializeField] private Image globalLevelPreview;
    [SerializeField] private TextMeshProUGUI globalLevelDescription;

    private void Awake()
    {
        if(localLevelPreview != null)
        {
            localLevelPreview.sprite = levelPreview;
        }
        else
        {
            Debug.LogError("No level preview image given for this\"" + this.name + "\"level selection button!");
        }
    }

    public void Clicked()
    {
        globalLevelPreview.sprite = levelPreview;

        //Language Support
        if(GameManager.instance != null)
        {
            globalLevelDescription.text = levelDescription[GameManager.instance.languageIndex];
        }
        else globalLevelDescription.text = levelDescription[0];
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
