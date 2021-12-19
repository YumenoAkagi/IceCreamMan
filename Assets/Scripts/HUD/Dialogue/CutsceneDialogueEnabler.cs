using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDialogueEnabler : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager.StartDialogue(true, dialogue);
    }
}
