using UnityEngine;
using UnityEngine.SceneManagement;

public class _Preload : MonoBehaviour
{
    public bool isLoaded = false;

    //Static instance check
    private void Awake()
    {
        if (SceneManager.GetSceneByName("_Preload").isLoaded)
        {
            isLoaded = true;
        }

        if (!isLoaded)
        {


            Debug.Log("Loaded preload");
            SceneManager.LoadScene(0, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }
}