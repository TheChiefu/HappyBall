/// <summary>
/// Loads the Preload scene
/// </summary>
public class _Preload : UnityEngine.MonoBehaviour
{
    public static _Preload instance;

    //Static instance check
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0, UnityEngine.SceneManagement.LoadSceneMode.Additive);

            //TESTING PURPOSES
            Utility.LimitFramerate(60, 1);
        }
        else Destroy(this);
    }
}