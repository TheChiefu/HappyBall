using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// Global SaveData class that manages data in the game.
/// Found in "Generic.cs"
/// </summary>
[Serializable]
public class SaveData
{
    public UserData ud;
    public PreferencesData pd;
    public List<LevelSaveData> levels;

    public SaveData()
    {
        this.ud = new UserData();
        this.pd = new PreferencesData();
        this.levels = new List<LevelSaveData>();
    }

    /// <summary>
    /// Saves object as is to JSON file
    /// </summary>
    /// <param name="path"></param>
    public bool Save(string path)
    {
        try
        {
            //Save current UseData to file
            string userJSON = JsonUtility.ToJson(this);
            File.WriteAllText(path, userJSON);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError("Could not save file at: " + path + "\nException: " + ex.Message);
            return false;
        }
    }


    /// <summary>
    /// When given new LevelSaveData add it to 'level' list and save to file
    /// </summary>
    /// <param name="path"></param>
    /// <param name="lsd"></param>
    public bool Save(string path, LevelSaveData lsd)
    {
        try
        {
            bool found = false;

            //Find if level is already in list, if it is has overwrite it.
            for (int i = 0; i < this.levels.Count; i++)
            {
                if (levels[i].index == lsd.index)
                {
                    levels[i].Overwrite(lsd);
                    found = true;
                    break;
                }
            }

            //If level is not in list add it
            if (!found) levels.Add(lsd);
            return Save(path);
        }
        catch (Exception ex)
        {
            Debug.LogError("Could not save file at: " + path + "\nException: " + ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Given new user data overwrite old one and then save
    /// </summary>
    /// <param name="path"></param>
    /// <param name="newData"></param>
    public bool Save(string path, UserData newData)
    {
        try
        {
            ud.Overwrite(newData);
            return Save(path);
        }
        catch (Exception ex)
        {
            Debug.LogError("Could not save user data.\nException: " + ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Given preferences data overwrite old one and save to file
    /// </summary>
    /// <param name="path"></param>
    /// <param name="newPrefs"></param>
    public bool Save(string path, PreferencesData newPrefs)
    {
        try
        {
            pd.Overwrite(newPrefs);
            return Save(path);
        }
        catch (Exception ex)
        {
            Debug.LogError("Could not save preference data.\nException: " + ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Attempt to load the user data from given path
    /// </summary>
    /// <param name="path"></param>
    public bool Load(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                //Get json data from path
                string json = File.ReadAllText(path);
                SaveData SD = JsonUtility.FromJson<SaveData>(json);

                //Overwrite data values
                this.ud.Overwrite(SD.ud);
                this.pd.Overwrite(SD.pd);
                
                //Clear then copy all new values to current level data list
                levels.Clear();
                for(int i = 0; i < SD.levels.Count; i++)
                {
                    this.levels.Add(SD.levels[i]);
                }

                return true;
            }

            //If no file exists save default one
            else
            {
                Debug.Log("No save file at: " + path + " creating one.");
                return Save(path);
            }

        }
        catch (Exception ex)
        {
            Debug.LogError("Could not save save at: " + path + "\nException: " + ex.Message);
            return false;
        }
    }
}



/// <summary>
/// Generic userdata class that stores user changable values.
/// Found in "Generic.cs"
/// </summary>
[Serializable]
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

    /// <summary>
    /// Overwrite current UserData object with hew UserData object's value
    /// </summary>
    /// <param name="ud"></param>
    public void Overwrite(UserData ud)
    {
        this.username = ud.username;
        this.ballColor = ud.ballColor;
    }
}


/// <summary>
/// Deals with level statistics. Found in "Generic.cs"
/// </summary>
[Serializable]
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
    public void Overwrite(LevelSaveData newData)
    {
        this.index = newData.index;
        this.name = newData.name;

        if (newData.totalScore > this.totalScore || this.totalScore <= 0)
            this.totalScore = newData.totalScore;

        if (newData.quickestTime > this.quickestTime || this.quickestTime <= 0)
            this.quickestTime = newData.quickestTime;

        if (newData.stars > this.stars || this.stars <= 0)
            this.stars = newData.stars;
    }
}

[Serializable]
public class PreferencesData
{
    public bool isFullscreen;
    public int resolutionIndex;
    public int fullscreenModeIndex;
    public int fpsCap;
    public int textureQualityIndex;
    public int anisotropicIndex;
    public int antiAliasIndex;
    public int shadowResIndex;
    public int shadowDistance;
    public int vsyncIndex;
    public int langIndex;

    //Default value
    public PreferencesData()
    {
        this.isFullscreen = false;
        this.resolutionIndex = 0;
        this.fullscreenModeIndex = 0;
        this.fpsCap = -1;
        this.textureQualityIndex = 0;
        this.anisotropicIndex = 0;
        this.antiAliasIndex = 0;
        this.shadowResIndex = 0;
        this.shadowDistance = 0;
        this.vsyncIndex = 0;
        this.langIndex = 0;
    }

    /// <summary>
    /// Manual overwrite of PreferenceData object with given new one
    /// </summary>
    /// <param name="newData"></param>
    public void Overwrite(PreferencesData newData)
    {
        this.isFullscreen = newData.isFullscreen;
        this.resolutionIndex = newData.resolutionIndex;
        this.fullscreenModeIndex = newData.fullscreenModeIndex;
        this.fpsCap = newData.fpsCap;
        this.textureQualityIndex = newData.textureQualityIndex;
        this.anisotropicIndex = newData.anisotropicIndex;
        this.antiAliasIndex = newData.antiAliasIndex;
        this.shadowResIndex = newData.shadowResIndex;
        this.shadowDistance = newData.shadowDistance;
        this.vsyncIndex = newData.vsyncIndex;
        this.langIndex = newData.langIndex;
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
        return Application.persistentDataPath + "/save.dat";
    }

    public static void SetGameSettings(PreferencesData pd)
    {

        //Set resolution
        Resolution temp = Screen.resolutions[pd.resolutionIndex];
        Screen.SetResolution(temp.width, temp.height, pd.isFullscreen);

        //Other
        Screen.fullScreen = pd.isFullscreen;
        Screen.fullScreenMode = (FullScreenMode)pd.fullscreenModeIndex;
        Application.targetFrameRate = pd.fpsCap;
        QualitySettings.anisotropicFiltering = (AnisotropicFiltering)pd.anisotropicIndex;
        QualitySettings.antiAliasing = pd.antiAliasIndex;
        QualitySettings.shadowResolution = (ShadowResolution)pd.shadowResIndex;
        QualitySettings.shadowDistance = pd.shadowDistance;
        QualitySettings.vSyncCount = pd.vsyncIndex;
        GameManager.instance.languageIndex = pd.langIndex;
    }
}