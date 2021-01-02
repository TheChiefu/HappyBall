[UnityEngine.CreateAssetMenu(fileName = "Multilanguage_SO", menuName = "Utility/Multi-language Object", order = 1)]
public class MultilanguageSO : UnityEngine.ScriptableObject
{
    public string[] English;
    public string[] Japanese;

    public string GetText(int index)
    {
        switch (index)
        {
            case 0:
                return GetString(English);
            case 1:
                return GetString(Japanese);
            default:
                return GetString(English);
        }
    }

    //Build single string from array of strings
    private string GetString(string[] array)
    {
        string output = string.Empty;

        for(int i = 0; i < array.Length; i++)
        {
            //Ensure last line doesn't have trailing endline
            if (i != array.Length - 1) output += array[i] + "\n";
            else output += array[i];
        }

        return output;
    }
}