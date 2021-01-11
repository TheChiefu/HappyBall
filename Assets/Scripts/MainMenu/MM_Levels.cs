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
    [SerializeField] private GameObject MainMenuCanvas;

    [SerializeField] private GameObject LevelSelectTemplate;
    [SerializeField] private RectTransform spawnParent;
    [SerializeField] private Sprite[] levelPreviews;
    [SerializeField] private Sprite defaultPreview;
    [SerializeField] private TextMeshProUGUI globalLevelDescription;
    [SerializeField] private Image globalLevelPreview;

    [Tooltip("Index is as follows: 0-Name, 1-Total Score, 2-Quickest time, 3-Stars, 4-NoInfo")]
    [SerializeField] private MultilanguageSO[] levelDescription;


    private int selectedLevelIndex = -1;
    private int buildIndexOffset = 2;
    private int langIndex = -1;

    private void Start()
    {
        langIndex = GameManager.instance.languageIndex;

        //Spawn Level Items in Grid area as clickable buttons
        for(int i = 0; i < (SceneManager.sceneCountInBuildSettings - buildIndexOffset); i++)
        {
            //Instanced version
            GameObject newClicker = Instantiate(LevelSelectTemplate, spawnParent);
            MM_LevelSelect info = newClicker.GetComponent<MM_LevelSelect>();
            info.levelPreview = levelPreviews[i];
            info.localLevelPreview.sprite = levelPreviews[i];
            info.levelIndex = i + buildIndexOffset;

            newClicker.GetComponent<MM_LevelSelect>().manager = this;
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

            string description = 
                (levelDescription[0].GetText(langIndex) + lsd.name) + 
                ("\n" + levelDescription[1].GetText(langIndex) + lsd.totalScore) + 
                ("\n" + levelDescription[2].GetText(langIndex) + lsd.quickestTime) + 
                ("\n" + levelDescription[3].GetText(langIndex) + lsd.stars);

            globalLevelDescription.text = description;
        }
        else globalLevelDescription.text = levelDescription[4].GetText(langIndex);

    }

    public void PressPlay()
    {
        InputManager.instance.ChangeToGameplay();

        SceneManager.LoadScene(selectedLevelIndex);
    }

    public void PressBack()
    {
        MainMenuCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
