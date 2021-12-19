using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> Scripts = new Queue<string>();
    public Text dialogueTextField, nameTextField;
    private bool isSceneDialogue = false;
    public GameObject DialoguePanel, nextText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextDialogue();
        }
    }

    public void StartDialogue(bool isSceneDialogue, Dialogue dialogue)
    {
        this.isSceneDialogue = isSceneDialogue;
        // if not scene dialogue, open dialogue panel
        if(!DialoguePanel.activeInHierarchy)
            DialoguePanel.SetActive(true);

        if(dialogue.Name != null || dialogue.Name != "")
        {
            nameTextField.text = dialogue.Name;
        } else
        {
            nameTextField.text = "";
        }

        foreach(string d in dialogue.Dialogues)
        {
            Scripts.Enqueue(d);
        }

        // start initial dialogue
        NextDialogue();
    }

    public void NextDialogue()
    {
        nextText.SetActive(false);
        if (Scripts.Count <= 0)
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
        {
            if(FindObjectOfType<MainMenu>() != null)
                FindObjectOfType<MainMenu>().LoadNextLevel();
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // skip to next scene
        }
        else
        {
            // close dialogue panel
            DialoguePanel.SetActive(false);
            // set timescale back to normal
            Time.timeScale = 1f;
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

        nextText.SetActive(true);
    }
}
