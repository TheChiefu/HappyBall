using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    [Header("Ball Customization")]
    public GameObject HappyBall;

    /// <summary>
    /// Given image, change HappyBall color to image color
    /// </summary>
    /// <param name="img"></param>
    public void ChangeColorFromImageButton(Image img)
    {
        HappyBall.GetComponent<MeshRenderer>().material.color = img.color;
    }
}
