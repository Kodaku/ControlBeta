using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool isDialogueStarted = false;
    private bool isDialogueFinished;

    public void ProcessInput()
    {
        if(Input.GetKeyDown(KeyCode.R) && !PauseMenu.GameIsPaused)
        {
            if(DialogueDirector.IsShowingDialogue)
            {
                ContinueDialogue();
            }
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue();
    }

    private void ContinueDialogue()
    {
        isDialogueFinished = FindObjectOfType<DialogueManager>().DisplayNextSentence();
        if(isDialogueFinished)
        {
            isDialogueStarted = false; // ready to start a new dialogue
        }
    }
}
