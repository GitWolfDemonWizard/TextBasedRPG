using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueUI : MonoBehaviour
{
    [Header("Dialogue Manager")]
    public DialogueManager DM;

    [Header("UI")]
    public TextMeshProUGUI SpeakerTextDisplay;
    public TextMeshProUGUI DialogueTextDisplay;
    public List<Button> Buttons;
    public List<TextMeshProUGUI> ButtonLabels;

    private void OnEnable()
    {
        DialogueManager.OnDialogueUpdated += UpdateUI;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueUpdated -= UpdateUI;
    }

    private void UpdateUI(string speaker, string dialogue, List<DialogueChoice> choices)
    {
        // Overrides previous speaker text for most recently called speaker text.
        SpeakerTextDisplay.text = speaker;
        // Overrides previous dialogue text for most recently called dialogue text.
        DialogueTextDisplay.text = dialogue;

        // Cycles through the button list for the number of buttons in that list. Then correspondingly displays the options.
        for (int i = 0; i < Buttons.Count; i++)
        {
            // If the buttons are available to display, then perform the inside.
            if (i < choices.Count)
            {
                // Displays the button.
                Buttons[i].gameObject.SetActive(true);
                // Updates the text on the button.
                ButtonLabels[i].text = choices[i].ChoiceText;
            }
            else
            {
                // Hides any buttons that aren't able to present data.
                Buttons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnChoiceClicked(int index)
    {
        DM.SelectChoice(index);
        // Resetting the button.
        EventSystem.current.SetSelectedGameObject(null);
    }
}
