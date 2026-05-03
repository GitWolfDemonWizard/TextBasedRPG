using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class DialogueChoice 
{
    [Header("UI")]
    public string ChoiceText; // The choice the player sees and can select

    [Header("Flow")]
    public string NextNodeID; // ID of the next node
    public bool ReloadScene = false;

    [Header("Conditions")]
    public List<string> RequiredFlags = new(); // All the needed flags for the quest to appear
    public List<string> ForbiddenFlags = new(); // Flags that will prevent a choice from being shown

    [Header("Flags on Select")]
    public List<string> GrantFlags = new(); // Flags that are added when the player selects this choice.
}
