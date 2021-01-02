public class MultilanguageText : UnityEngine.MonoBehaviour
{
    public string English = "N/A";
    public string Japanese = "N/A";
    public MultilanguageSO PremadeLines = null;

    [UnityEngine.HideInInspector]
    public string outputText = string.Empty;

    public void Start()
    {
        int index = GameManager.instance.languageIndex;
        TMPro.TextMeshProUGUI DisplayText = GetComponent<TMPro.TextMeshProUGUI>();

        //If premade lines are given use them instead of local lines
        if(PremadeLines != null) DisplayText.text = PremadeLines.GetText(index);
        else
        {
            switch (index)
            {
                case 0:
                    DisplayText.text = English;
                    outputText = English;
                    break;
                case 1:
                    outputText = Japanese;
                    DisplayText.text = Japanese;
                    break;
                default:
                    outputText = English;
                    DisplayText.text = English;
                    break;
            }
        } 
    }
}
