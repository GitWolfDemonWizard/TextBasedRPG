using UnityEngine;
using TMPro;

public class GameSaveSystemManager : MonoBehaviour
{
    public DialogueManager DialogueManager;

    private GameData _currentGameData = new();

    private float _timePassed;

    public TextMeshProUGUI SaveText;

    private void Start()
    {
        SaveText.text = "";
    }
    public void SaveGame()
    {
        _currentGameData.PlayData = DialogueManager.ToData();
        SaveManager.Save(_currentGameData);
        Debug.Log("Game Saved");
        SaveText.text = "Game Saved!";
        _timePassed = 0;
    }

    public void LoadGame()
    {
        if (SaveManager.TryLoad(out _currentGameData))
        {
            DialogueManager.FromData(_currentGameData.PlayData);
            Debug.Log("Game Loaded");
            DialogueManager.GoToNode(_currentGameData.PlayData.CurrentNode.NodeID);
            SaveText.text = "Game Loaded!";
            _timePassed = 0;
        }
        else
        {
            Debug.LogWarning("No Save found. Creating a new one.");
            SaveGame();
        }
    }

    private void Update()
    {
        _timePassed += Time.deltaTime;

        if (_timePassed > 5.0)
        {
            SaveText.text = "";
        }
    }
}
