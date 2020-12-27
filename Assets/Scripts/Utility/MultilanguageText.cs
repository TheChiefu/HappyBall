public class MultilanguageText : UnityEngine.MonoBehaviour
{
    public string English = "N/A";
    public string Japanese = "N/A";

    [UnityEngine.HideInInspector]
    public string outputText = string.Empty;

    public void Start()
    {
        switch (GameManager.instance.languageIndex)
        {
            case 0:
                GetComponent<TMPro.TextMeshProUGUI>().text = English;
                outputText = English;
                break;
            case 1:
                outputText = Japanese;
                GetComponent<TMPro.TextMeshProUGUI>().text = Japanese;
                break;
            default:
                outputText = English;
                GetComponent<TMPro.TextMeshProUGUI>().text = English;
                break;
        }
    }
}
