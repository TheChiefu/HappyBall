using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Generic gameobject class that contains level information
/// </summary>
public class MM_LevelSelect : MonoBehaviour
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
