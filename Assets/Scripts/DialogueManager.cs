using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Data")]
    public DialogueDatabase DialogueDatabase; // List of dialogue nodes.
    public FlagManager FlagManager; // Hashset of string value for the player.
    public string StartNodeID; // Scriptable object's id that starts the game

    public delegate void DialogueUpdated(string speakerName, string dialogueText, List<DialogueChoice> choices);
    public static event DialogueUpdated OnDialogueUpdated;

    private DialogueNode _currentNode; // Current node that the game is on

    // Takes the data from the Dialogue Manager to be able to act as the source of truth for a PlayData object
    public PlayData ToData()
    {
        //Setting the data in PlayData to match what is in this system.
        return new PlayData
        {
            CurrentNode = _currentNode,
            CurrentFlags = FlagManager.Flags
        };
    }

    public void FromData(PlayData data)
    {
        // Pulling the data from the data stored to properly input it
        _currentNode = data.CurrentNode;
        foreach(var flag in data.CurrentFlags)
        {
            // Adds the stored flags to the system so that players have the correct flags for their loaded run
            FlagManager.AddFlag(flag);
        }
    }

    // Starting our first node.
    private void Start()
    {
        // Calls the first scene in the game so that it starts.
        GoToNode(StartNodeID);
    }

    // Reloads the current scene
    private void ReloadScene()
    {
        // Gets the current scene and stores it as a temporary variable.
        var currentScene = SceneManager.GetActiveScene();
        // Loads the scene by using the string that is the scene name.
        SceneManager.LoadScene(currentScene.name);
    }

    // Check choices are available.
    private bool IsChoiceAvailable(DialogueChoice choice)
    {
        // Check the required flags for the dialogue node, and if there is not a required flag, then return false.
        foreach (var required in choice.RequiredFlags)
        {
            if (!FlagManager.HasFlag(required)) return false;
        }
        // Check the forbidden flags for the dialogue node, and if there is a forbidden flag, then return false.
        foreach (var forbidden in choice.ForbiddenFlags)
        {
            if (FlagManager.HasFlag(forbidden)) return false;
        }
        // Otherwise return true.
        return true;
    }

    // Filtering choices based on availability.
    private List<DialogueChoice> FilterChoices(List<DialogueChoice> choices)
    {
        // Generate a new list withou any new choices in it.
        var result = new List<DialogueChoice>();

        // Cycle through each choice to see if it is available and add it to the list if it is.
        foreach (var choice in choices)
        {
            // Using the IsChoiceAvailable function to check the availability of a choice by using flag filtering.
            if (IsChoiceAvailable(choice))
            {
                // If the choice is available, by not having forbidden flags and having the required flags, add choice to the list.
                result.Add(choice);
            }
        }
        // Returns the list after filtering the choices.
        return result;
    }

    // Setting up the choices for selection functionality.
    public void SelectChoice(int index)
    {
        var filtered = FilterChoices(_currentNode.Choices);
        var choice = filtered[index];

        // Apply flags based on the choice
        foreach (var flag in choice.GrantFlags)
        {
            FlagManager.AddFlag(flag);
        }
        // Reloads the scene if the player's choice reloads the scene.
        if (choice.ReloadScene)
        {
            ReloadScene();
            return;
        }
        // Goes to the next node based on the player's choice.
        GoToNode(choice.NextNodeID);
    }

    public void GoToNode(string nodeID)
    {
        // Sets the current node to the node that is being input through the function.
        _currentNode = DialogueDatabase.GetNode(nodeID);

        if(_currentNode == null)
        {
            // A way to tell the player that the dialogue has ended.
            OnDialogueUpdated?.Invoke("","[Dialogue Ended]", _currentNode.Choices);
            return;
        }

        var filtered = FilterChoices(_currentNode.Choices);
        // Updates the UI with the new choices
        OnDialogueUpdated?.Invoke(_currentNode.SpeakerName, _currentNode.DialogueText, filtered);
    }
}
