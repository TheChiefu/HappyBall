using UnityEngine;

public class MM_Credits : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;

    public void Back()
    {
        MainMenuCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
