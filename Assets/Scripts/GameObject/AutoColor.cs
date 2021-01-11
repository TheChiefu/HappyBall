using UnityEngine;

/// <summary>
/// This script automatically colors a given object and its child objects with the LevelManager's color scheme
/// </summary>
public class AutoColor : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] primaryObjects;
    [SerializeField] private MeshRenderer[] secondaryObjects;
    [SerializeField] private MeshRenderer[] tertiaryObjects;
    

    private void Start()
    {
        if(LevelManager.instance != null)
        {
            TextureObjects(primaryObjects, LevelManager.instance.PrimaryTexture);
            TextureObjects(secondaryObjects, LevelManager.instance.SecondaryTexture);
            TextureObjects(tertiaryObjects, LevelManager.instance.TertiaryTexture);

            ColorObject(primaryObjects, LevelManager.instance.PrimaryColor);
            ColorObject(secondaryObjects, LevelManager.instance.SecondaryColor);
            ColorObject(tertiaryObjects, LevelManager.instance.TertiaryColor);
        }
        else
        {
            Debug.LogWarning("No level manager on this scene, using default material colors");
        }

    }

    /// <summary>
    /// Given a mesh array all the items are given a texture
    /// </summary>
    /// <param name="array"></param>
    /// <param name="texArrayItem"></param>
    private void TextureObjects(MeshRenderer[] array, Texture texArrayItem)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i].material.mainTexture = texArrayItem;
        }
    }

    /// <summary>
    /// Given a mesh array all the items are given a color
    /// </summary>
    /// <param name="array"></param>
    /// <param name="assignedColor"></param>
    private void ColorObject(MeshRenderer[] array, Color assignedColor)
    {
        for(int i = 0; i < array.Length; i++)
        {
            array[i].material.color = assignedColor;
        }
    }
}