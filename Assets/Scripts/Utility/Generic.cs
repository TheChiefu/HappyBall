using System;
using System.Collections.Generic;
using System.IO;
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
    public Dictionary<int, LevelSaveData> levelsCompleted;


    //Constructors
    public UserData()
    {
        this.username = "User";
        this.ballColor = Color.yellow;
        this.levelsCompleted = new Dictionary<int, LevelSaveData>();
    }

    public UserData(string username, Color ballColor, Dictionary<int, LevelSaveData> levels)
    {
        this.username = username;
        this.ballColor = ballColor;
        this.levelsCompleted = levels;
    }
    

    public void Save(string path)
    {
        try
        {
            //Save current UseData to file
            string userJSON = JsonUtility.ToJson(this);
            File.WriteAllText(path, userJSON);
        }
        catch (Exception ex)
        {
            Debug.LogError("Could not save file at: " + path + "\nException: " + ex.Message);
        }
    }

    /// <summary>
    /// Save user data to path, except update Level Save Data dictionary
    /// </summary>
    /// <param name="path"></param>
    /// <param name="lsd"></param>
    public void Save(string path, LevelSaveData lsd)
    {
        try
        {
            if(lsd == null)
            {
                lsd = new LevelSaveData();
                Debug.Log("Created new level save data");
            }
            else
            {
                //Find if level is already in dictionary, if it is has overwrite it.
                if (this.levelsCompleted.ContainsKey(lsd.index))
                    this.levelsCompleted[lsd.index].Overwrite(lsd);
                else
                    this.levelsCompleted.Add(lsd.index, lsd);
            }

            //Need to reserialize due to Dictiontary's not being directly serializable

            //Save current UseData to file
            string userJSON = JsonUtility.ToJson(this);
            File.WriteAllText(path, userJSON);
        }
        catch (Exception ex)
        {
            Debug.LogError("Could not save file at: " + path + "\nException: " + ex.Message);
        }
    }


    private void SeralizeUserData()
    {

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
                this.levelsCompleted = ud.levelsCompleted;
            }
            else
            {
                Save(path, null);
            }
            
        }
        catch(Exception ex)
        {
            Debug.LogError("Could not save save at: " + path + "\nException: " + ex.Message);
        }
    }
}


/// <summary>
/// Deals with level statistics. Found in "Generic.cs"
/// </summary>
[System.Serializable]
public class LevelSaveData
{
    public int index;
    public string name;
    public int totalScore;
    public int quickestTime;
    public int stars;

    public LevelSaveData()
    {
        this.index = -1;
        this.name = string.Empty;
        this.totalScore = 0;
        this.quickestTime = 0;
        this.stars = 0;
    }

    public LevelSaveData(int index, string name, int totalScore, int quickestTime, int stars)
    {
        this.index = index;
        this.name = name;
        this.totalScore = totalScore;
        this.quickestTime = quickestTime;
        this.stars = stars;
    }

    //Overwrite the current object version with new information (check for better results)
    public void Overwrite (LevelSaveData newData)
    {
        this.index = newData.index;
        this.name = newData.name;

        if(newData.totalScore > this.totalScore || this.totalScore <= 0)
            this.totalScore = newData.totalScore;

        if(newData.quickestTime > this.quickestTime || this.quickestTime <= 0)
            this.quickestTime = newData.quickestTime;

        if(newData.stars > this.stars || this.stars <= 0)
            this.stars = newData.stars;
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