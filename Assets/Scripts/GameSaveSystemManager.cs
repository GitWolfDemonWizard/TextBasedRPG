using UnityEngine;

public class GameSaveSystemManager : MonoBehaviour
{
    public DialogueManager DialogueManager;

    private GameData _currentGameData = new();

    public void SaveGame()
    {
        _currentGameData.PlayData = DialogueManager.ToData();
        SaveManager.Save(_currentGameData);
        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (SaveManager.TryLoad(out _currentGameData))
        {
            DialogueManager.FromData(_currentGameData.PlayData);
            Debug.Log("Game Loaded");
            DialogueManager.GoToNode(_currentGameData.PlayData.CurrentNode.NodeID);
        }
        else
        {
            Debug.LogWarning("No Save found. Creating a new one.");
            SaveGame();
        }
    }
}
