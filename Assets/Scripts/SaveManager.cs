using System;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static readonly string PathToFile = Application.persistentDataPath + "/GameSaveFile.json";

    // Function to save the game data, such as version, date saved and current node
    public static void Save(GameData gameData)
    {
        gameData.Version = Application.version;

        gameData.LastSaved = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

        string json = JsonUtility.ToJson(gameData, true);

        File.WriteAllText(PathToFile, json);

        Debug.Log($"[SaveManager saved game data to {PathToFile}");
    }

    public static GameData Load()
    {
        TryLoad(out GameData gameData);
        return gameData;
    }

    public static bool TryLoad(out GameData gameData)
    {
        try
        {
            if (!File.Exists(PathToFile))
            {
                gameData = new GameData();
                return false;
            }

            string json = File.ReadAllText(PathToFile);
            gameData = JsonUtility.FromJson<GameData>(json);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"[SaveManager] Load failed: {e.Message}");
            gameData = new GameData();
            throw;
        }
    }
}
