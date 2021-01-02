using System.Collections;
using System.Collections.Generic;
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

        TextureObjects(primaryObjects, LevelManager.instance.PrimaryTexture);
        TextureObjects(secondaryObjects, LevelManager.instance.SecondaryTexture);
        TextureObjects(tertiaryObjects, LevelManager.instance.TertiaryTexture);

        ColorObject(primaryObjects, LevelManager.instance.PrimaryColor);
        ColorObject(secondaryObjects, LevelManager.instance.SecondaryColor);
        ColorObject(tertiaryObjects, LevelManager.instance.TertiaryColor);

    }

    private void TextureObjects(MeshRenderer[] array, Texture texArrayItem)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i].material.mainTexture = texArrayItem;
        }
    }

    private void ColorObject(MeshRenderer[] array, Color assignedColor)
    {
        for(int i = 0; i < array.Length; i++)
        {
            array[i].material.color = assignedColor;
        }
    }
}