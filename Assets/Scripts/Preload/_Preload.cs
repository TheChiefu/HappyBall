/// <summary>
/// Loads the Preload scene
/// </summary>
public class _Preload : UnityEngine.MonoBehaviour
{
    private void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0, UnityEngine.SceneManagement.LoadSceneMode.Additive);

        //TESTING PURPOSES
        Utility.LimitFramerate(60, 1);
    }
}