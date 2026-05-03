using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Database")]
public class DialogueDatabase : ScriptableObject
{
    // The list of dialogue nodes within the particular database.
    public List<DialogueNode> Nodes = new();

    // Dictionary for looking up quickly the data on a node based on the NodeID Key.
    private Dictionary<string, DialogueNode> _lookup;

    // Building the dictionary
    private void BuildNodeDictionary()
    {
        // If the dictionary exists, doesn't create a new dictionary.
        if (_lookup != null) return;
        // Otherwise, creates a new instance of the dictionary.
        _lookup = new();
        // Cycle through the list of nodes, and adds each node to the dicitionary with the NodeID as the key.
        foreach (DialogueNode node in Nodes)
        {
            _lookup.Add(node.NodeID, node);
        }
    }

    // Create a function for getting a node forom the dictionary based on the ID.
    public DialogueNode GetNode(string id)
    {
        // If the ID doesn't exist, return null.
        if (string.IsNullOrEmpty(id)) return null;
        // Only build the dictionary if it doesn't already exist.
        BuildNodeDictionary();
        // Try to fetch the node from the dictionary, and if found, hold it in a temporary variable.
        _lookup.TryGetValue(id, out DialogueNode node);
        // Return the node.
        return node;
    }

}
