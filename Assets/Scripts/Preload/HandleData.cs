using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// As name implies it handles data, but handles user configuration and optionss. Then saves to file.
/// </summary>
public class HandleData : MonoBehaviour
{
    [SerializeField] private UserData userData = new UserData();

    public void SaveJSON()
    {
        string userJSON = JsonUtility.ToJson(userData);
        System.IO.File.WriteAllText(Utility.GetUserSavePath(), userJSON);
    }

    public  void LoadJSON()
    {
        string json = System.IO.File.ReadAllText(Utility.GetUserSavePath());
        UserData ud = JsonUtility.FromJson<UserData>(json);
        userData.username = ud.username;
        userData.ballColor = ud.ballColor;
    }

    private void Awake()
    {
        LoadJSON();
    }
}
