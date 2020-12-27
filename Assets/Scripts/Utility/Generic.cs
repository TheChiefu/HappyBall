using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;



/// <summary>
/// Generic userdata class that stores user changable values.
/// Found in "Generic.cs"
/// </summary>
[System.Serializable]
public class UserData
{
    public string username;
    public Color ballColor;


    //Constructors
    public UserData()
    {
        this.username = "User";
        this.ballColor = Color.yellow;
    }

    public UserData(string username, Color ballColor)
    {
        this.username = username;
        this.ballColor = ballColor;
    }
    
    /// <summary>
    /// Attempt to user data from given path
    /// </summary>
    /// <param name="path"></param>
    public void Save(string path)
    {
        try
        {
            string userJSON = JsonUtility.ToJson(this);
            File.WriteAllText(path, userJSON);
        }
        catch(Exception ex)
        {
            Debug.LogError("Could not save file at: " + path + "\nException: " + ex.Message);
        }
        
    }

    /// <summary>
    /// Attempt to load the user data from given path
    /// </summary>
    /// <param name="path"></param>
    public void Load(string path)
    {
        try
        {
            if(File.Exists(path))
            {
                string json = File.ReadAllText(path);
                UserData ud = JsonUtility.FromJson<UserData>(json);
                this.username = ud.username;
                this.ballColor = ud.ballColor;
            }
            else
            {
                Save(path);
            }
            
        }
        catch(Exception ex)
        {
            Debug.LogError("Could not save save at: " + path + "\nException: " + ex.Message);
        }
    }
}


/// <summary>
/// Utility class for various functions in game and scripting.
/// Found in "Generic.cs"
/// </summary>
public static class Utility
{ 
    public static void LimitFramerate(int fps, int vsync)
    {
        Application.targetFrameRate = fps;
        QualitySettings.vSyncCount = vsync;
    }

    public static string GetUserSavePath()
    {
        return Application.persistentDataPath + "/usersave.txt";
    }
}