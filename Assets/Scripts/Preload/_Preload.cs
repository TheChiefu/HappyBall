using UnityEngine;
using UnityEngine.SceneManagement;

public class _Preload : MonoBehaviour
{
    public static _Preload instance = null;

    //Static instance check
    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogWarning("Destroying: " + this.name);
            Destroy(this);
        }

        Debug.Log("Is scene Loaded: " + SceneManager.GetSceneByName("_Preload").isLoaded);

        if (SceneManager.GetSceneByName("_Preload").isLoaded)
        {
            Debug.Log("Didn't load preload");
        }
        else
        {
            Debug.Log("Loaded preload");
            SceneManager.LoadScene(0, LoadSceneMode.Additive);
        }
    }
}