using System;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static readonly string PathToFile = Application.persistentDataPath + "/GameSaveFile.json";

    // Function to save the game data, such as version, date saved and current node
    public static void Save(GameData gameData)
    {
        // Setting the saved application version.
        gameData.Version = Application.version;
        // Saving the date of the save.
        gameData.LastSaved = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        // Sending the data to a json format to be stored properly for future callback.
        string json = JsonUtility.ToJson(gameData, true);
        // Storing the data in its location.
        File.WriteAllText(PathToFile, json);
        // Sending a notification through the debug console that the process works
        Debug.Log($"[SaveManager saved game data to {PathToFile}.");
    }

    // Function to load up a saved file of the game.
    public static GameData Load()
    {
        // Attempting the TryLoad Function to load up a save file.
        TryLoad(out GameData gameData);
        // Returning the data from the game data
        return gameData;
    }

    // Public boolean function that outputs the data, while also returning if the data exists and otherwise creating it if it doesn't.
    public static bool TryLoad(out GameData gameData)
    {
        try
        {
            // Checks if the data exists already.
            if (!File.Exists(PathToFile))
            {
                // Creates a new instance of GameData if it doesn't already exist.
                gameData = new GameData();
                // Making sure that the boolean returns a false value.
                return false;
            }
            // Reads the data from the json file if it exists
            string json = File.ReadAllText(PathToFile);
            // Takes the data and turns it into a GameData that can be used for the game.
            gameData = JsonUtility.FromJson<GameData>(json);
            // Returns a true result for output from the boolean.
            return true;
        }
        catch (Exception e)
        {
            // Catches if there is an error and puts out a message to the debug to tell what the source of the debug is.
            Debug.LogError($"[SaveManager] Load failed: {e.Message}");
            // Creates a new GameData in the chance of a corrupted save file.
            gameData = new GameData();
            throw;
        }
    }
}
