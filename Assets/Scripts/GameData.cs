using UnityEngine;
[System.Serializable]

public class GameData
{
    public string Version = "v1.1"; // Version number of the game
    public string LastSaved; // Last date that the game was saved.

    public PlayData PlayData = new();
}
