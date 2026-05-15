using UnityEngine;
using TMPro;

public class GameSaveSystemManager : MonoBehaviour
{
    // Has the dialogue manager to call any information from it.
    public DialogueManager DialogueManager;
    // Makes a new GameData asset to use with started up.
    private GameData _currentGameData = new();
    // Float value for time to make sure the text notifying a save doesn't remain visible the whole time.
    private float _timePassed;
    // Text to imform the player that their save actually occurs.
    public TextMeshProUGUI SaveText;

    private void Start()
    {
        // Setting the Save notification text to not be visible so that players aren't seeing things they needn't see.
        SaveText.text = "";
    }
    // Public function that can be called for a player to be able to save the game.
    public void SaveGame()
    {
        // Uses the function in the dialogue manager to take the current data to set the variable of current game data.
        _currentGameData.PlayData = DialogueManager.ToData();
        // Calls the function to save the game from the game manager.
        SaveManager.Save(_currentGameData);
        // Sends a message to the debug system.
        Debug.Log("Game Saved");
        // Creates a message that the player can see.
        SaveText.text = "Game Saved!";
        // Resets the time that hides the visual message for the save function.
        _timePassed = 0;
    }

    // Function to load the game
    public void LoadGame()
    {
        // Calls the TryLoad function to output the current game data as the saved data
        if (SaveManager.TryLoad(out _currentGameData))
        {
            // Calls the function the pulls the data from the saved data and commits it to the locations it is needed.
            DialogueManager.FromData(_currentGameData.PlayData);
            // Sends a message in the debug system.
            Debug.Log("Game Loaded");
            // Calls the function to go to the correct node for the player to play from.
            DialogueManager.GoToNode(_currentGameData.PlayData.CurrentNode.NodeID);
            // Shows a visual message to the player so that they know that they loaded the game.
            SaveText.text = "Game Loaded!";
            // Resets the time that hides the visual message for the save function.
            _timePassed = 0;
        }
        else
        {
            // Sends a warning to the debug system.
            Debug.LogWarning("No Save found. Creating a new one.");
            // Calls the save game function to be able to fix the missing load file
            SaveGame();
        }
    }

    private void Update()
    {
        // Adding time to the timer for shutting down the visual
        _timePassed += Time.deltaTime;
        // Checking if 5 seconds have passed.
        if (_timePassed > 5.0)
        {
            // Setting the text of the save notifier for the player invisible to save on some processing power
            SaveText.text = "";
        }
    }
}
