using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class PlayData
{
    public DialogueNode CurrentNode; // Current node open in the game
    public List<string> CurrentFlags; // Current flags of the run open in the game
}
