using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    private HashSet<string> _flags = new();

    // Method to check if a flag is present.
    public bool HasFlag(string flag)
    {
        // Returns a true/false based on whether the argument passed through is in the hashset.
        return _flags.Contains(flag);
    }

    // Method to add or grant flags
    public void AddFlag(string flag)
    {
        // If the flag being added is empty or a null, doesn't add the flag.
        if (string.IsNullOrEmpty(flag)) return;
        // Otherwise adds the string of the flag to the hashset.
        _flags.Add(flag);
    }
}
