using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Header("Player Data")]
    public UserData userData = new UserData();
    [SerializeField]
    private GameObject userDataInputPanel = null;

    [Header("Other")]
    public int languageIndex = 0;   //0 - English / 1 - Japanese
    private string applicationPath = string.Empty;
    public Camera mainCamera = null;


    private void Awake()
    {
        Initialize();
        mainCamera = Camera.main;
        applicationPath = Application.persistentDataPath;
        userData.Load(Utility.GetUserSavePath());
    }




    /// <summary>
    /// Check for other instances of GameManager script
    /// </summary>
    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogWarning("Instance of GameManager already exists, destroying new one.");
            Destroy(this);
        }
    }
}
