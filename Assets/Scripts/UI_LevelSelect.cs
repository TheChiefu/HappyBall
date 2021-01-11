using UnityEngine;
using UnityEngine.UI;

public class UI_LevelSelect : MonoBehaviour
{
    [Header("Level Attributes:")]
    public Sprite levelPreview;
    public int levelIndex;
    public MM_Levels manager;
    public Image localLevelPreview;

    public void Clicked()
    {
        manager.LevelSelected(levelIndex, levelPreview); 
    }
}
