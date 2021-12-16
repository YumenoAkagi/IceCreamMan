using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> Scripts = new Queue<string>();
    public Text dialogueTextField;
    private bool isSceneDialogue = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextDialogue();
        }
    }

    public void StartDialogue(bool isSceneDialogue, Dialogue dialogue)
    {
        foreach(string d in dialogue.Dialogues)
        {
            Scripts.Enqueue(d);
        }

        // start initial dialogue
        NextDialogue();
    }

    public void NextDialogue()
    {
        if(Scripts.Count <= 0)
        {
            StopDialogue();
            return;
        }

        string d = Scripts.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypingEffects(d));
    }

    public void StopDialogue()
    {
        if (isSceneDialogue)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // skip to next scene
        else
        {
            // close dialogue panel
        }
    }

    IEnumerator TypingEffects(string dialogue)
    {
        dialogueTextField.text = "";

        foreach(char letter in dialogue.ToCharArray())
        {
            dialogueTextField.text += letter;
            yield return null;
        }
    }
}
