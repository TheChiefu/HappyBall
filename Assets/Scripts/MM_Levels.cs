using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the Level Select screen and its elements
/// </summary>
public class MM_Levels : MonoBehaviour
{
    [SerializeField] private GameObject LevelSelectTemplate;
    [SerializeField] private RectTransform spawnParent;
    [SerializeField] private Sprite[] levelPreviews;
    [SerializeField] private Sprite defaultPreview;
    [SerializeField] private TextMeshProUGUI globalLevelDescription;
    [SerializeField] private Image globalLevelPreview;

    private int selectedLevelIndex = -1;
    private int buildIndexOffset = 2;

    private void Start()
    {
        //Spawn Level Items in Grid area as clickable buttons
        for(int i = 0; i < (SceneManager.sceneCountInBuildSettings - buildIndexOffset); i++)
        {
            //Instanced version
            GameObject newClicker = Instantiate(LevelSelectTemplate, spawnParent);
            UI_LevelSelect info = newClicker.GetComponent<UI_LevelSelect>();
            info.levelPreview = levelPreviews[i];
            info.localLevelPreview.sprite = levelPreviews[i];
            info.levelIndex = i + buildIndexOffset;

            newClicker.GetComponent<UI_LevelSelect>().manager = this;
        }
    }

    public void LevelSelected(int index, Sprite preview)
    {
        selectedLevelIndex = index;

        globalLevelPreview.sprite = preview;

        //Language Support
        if (GameManager.instance != null)
        {
            //Get Level stats
            List<LevelSaveData> levels = GameManager.instance.userData.levelsCompleted;
            LevelSaveData lsd = new LevelSaveData();

            //From list of levls find the correct level information by index
            for (int i = 0; i < levels.Count; i++)
            {
                if (levels[i].index == index)
                {
                    lsd.Overwrite(levels[i]);
                    break;
                }
            }

            globalLevelDescription.text = string.Format("Level Name: {0}\nBest Time: {1}\nHighest Score: {2}\nStars: {3}", lsd.name, lsd.quickestTime, lsd.totalScore, lsd.stars);
        }
        else globalLevelDescription.text = "No level info";

    }

    public void LoadLevel()
    {
        InputManager.instance.ChangeToGameplay();

        SceneManager.LoadScene(selectedLevelIndex);
    }
}
