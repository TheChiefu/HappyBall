public class MultilanguageText : UnityEngine.MonoBehaviour
{
    public string English = string.Empty;
    public string Japanese = string.Empty;

    public void Start()
    {
        switch (GameManager.instance.languageIndex)
        {
            case 0:
                GetComponent<TMPro.TextMeshProUGUI>().text = English;
                break;
            case 1:
                GetComponent<TMPro.TextMeshProUGUI>().text = Japanese;
                break;
            default:
                GetComponent<TMPro.TextMeshProUGUI>().text = English;
                break;
        }
    }
}
