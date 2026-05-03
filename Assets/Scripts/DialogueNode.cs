using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueNode")]
public class DialogueNode : ScriptableObject
{
    [Header("Identity")]
    public string NodeID; // Unique ID, "speakerName_intro_001.01"

    [Header("Dialogue")]
    public string SpeakerName; // Name of the character speaking
    [TextArea(2,5)]
    public string DialogueText; // What the speaker is saying.

    [Header("Choices")]
    public List<DialogueChoice> Choices = new(); // List of choices available to the player.
}
