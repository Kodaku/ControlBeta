using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Image dialogueBox;
    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;
    private Queue<string> names;
    private Queue<string> events;
    public DialogueReader dialogueReader;
    private SimpleDialogue[] dialogues;
    private float dialogueTextHeight;
    private int maximumCharactersBeforeOverflow;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        events = new Queue<string>();
        dialogueTextHeight = dialogueText.rectTransform.rect.height;
        // print("Dialogue Text height: " + dialogueTextHeight);
        maximumCharactersBeforeOverflow = StressTextBox();
        // print("Before Overflow: " + maximumCharactersBeforeOverflow);
        // LoadDialogue();
    }
    
    private int StressTextBox()
    {
        int currentLength = 1;
        string testSentence = "Z";
        bool overflow = false;
        while(!overflow)
        {
            dialogueText.text = testSentence;
            if(dialogueText.preferredHeight > dialogueTextHeight)
            {
                overflow = true;
            }
            else
            {
                testSentence += "Z";
                currentLength += 1;
            }
        }
        return currentLength;
    }

    public void LoadDialogue(int fileIndex)
    {
        names.Clear();
        sentences.Clear();
        events.Clear();
        dialogues = dialogueReader.ReadDialogues(fileIndex);
        // Debug.Log("Starting Conversation with " + dialogue.name);
        foreach(SimpleDialogue dialogue in dialogues)
        {
            string sentence = dialogue.sentence;
            string eventCategory = dialogue.eventCategory;
            // print("Analyzing " + sentence);
            // print("Sentence Length: " + sentence.Length);
            dialogueText.text = sentence;
            float preferredHeight = dialogueText.preferredHeight;
            if(preferredHeight > dialogueTextHeight)
            {
                // print((float)sentence.Length / maximumCharactersBeforeOverflow);
                int times = (int)Mathf.Ceil((float)sentence.Length / maximumCharactersBeforeOverflow);
                // print("Times: " + times);
                for(int i = 0; i < times; i++)
                {
                    int startIndex = (int)(i * maximumCharactersBeforeOverflow);
                    // print("Start Index: " + startIndex);
                    string subSentence = sentence.Substring(
                        startIndex,
                        Mathf.Min(maximumCharactersBeforeOverflow, sentence.Length - i * maximumCharactersBeforeOverflow)
                    );
                    // print("Sub Sentence: " + subSentence);
                    names.Enqueue(dialogue.characterName);
                    sentences.Enqueue(subSentence);
                    if(i == times - 1)
                    {
                        events.Enqueue(eventCategory);
                    }
                    else
                    {
                        events.Enqueue("");
                    }
                }
            }
            else
            {
                names.Enqueue(dialogue.characterName);
                sentences.Enqueue(sentence);
                events.Enqueue(eventCategory);
            }
        }
        names.Enqueue("");
        sentences.Enqueue("");
        events.Enqueue("");
        // print("Names Length: " + names.Count);
        // print("Sentences Length: " + sentences.Count);
        // print("Events Length: " + events.Count);
        StartDialogue();
        

        // DisplayNextSentence();
    }

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.Y))
    //     {
    //         while(sentences.Count != 2)
    //         {
    //             sentences.Dequeue();
    //             events.Dequeue();
    //             names.Dequeue();
    //             DisplayNextSentence();
    //         }
    //     }
    // }

    public void StartDialogue()
    {
        if(names.Count != 0 && sentences.Count != 0)
        {
            dialogueBox.gameObject.SetActive(true);
            DisplayNextSentence();
        }
    }

    public bool DisplayNextSentence()
    {
        try
        {
            string sentence = sentences.Dequeue();
            string name = names.Dequeue();
            string eventCategory = events.Dequeue();

            dialogueText.text = sentence;
            nameText.text = name;
            
            ProcessEvent(eventCategory);
        } catch(InvalidOperationException)
        {
            EndDialogue();
            return true;
        }

        if(sentences.Count == 0)
        {
            EndDialogue();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void EndDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
        // Debug.Log("End of conversation");
        DialogueDirector.IsShowingDialogue = false;
    }

    private void ProcessEvent(string eventCategory)
    {
        if(eventCategory == "BATTLE_START")
        {
            GameManager.ShowTransitionScreen();
        }
        else if(eventCategory == "BATTLE_BOSS")
        {
            GameManager.IsBossBattle = true;
            GameManager.ShowTransitionScreen();
        }
        else if(eventCategory == "SHOW_NEXT_TRIGGER")
        {
            GameManager.LoadNextSceneTrigger();
        }
        else if(eventCategory == "SWITCH_CHARACTER")
        {
            GameManager.SwitchCharacter();
        }
        else if(eventCategory == "ACTIVATE_ERICK_POWER")
        {
            GameManager.HasErickPower = true;
        }
        else if(eventCategory == "ACTIVATE_GARY_POWER")
        {
            GameManager.HasGaryPower = true;
        }
        else if(eventCategory == "BRUCE_APPEARS")
        {
            // print("Spawn Wave");
            SpecialAttackTargetManager.ClearTargetNames();
            GameManager.SpawnFirstWave();
            GameManager.SpawnWave();
        }
        else if(eventCategory == "CAN_SWITCH_CHARACTER")
        {
            GameManager.IsDoubleControlEnabled = true;
        }
        else if(eventCategory == "SPAWN_AND_SWITCH")
        {
            print("Spawn Another");
            // GameManager.IsControllingGary = true;
            // GameManager.IsControllingErick = false;
            if(GameManager.IsControllingErick)
            {
                GameManager.SwitchCharacter();
            }
            GameManager.IsDoubleControlEnabled = false;
            SpecialAttackTargetManager.ClearTargetNames();
            GameManager.SpawnFirstWave();
            GameManager.SpawnWave();
        }
        else if(eventCategory == "CONCLUSION")
        {
            GameManager.ShowConclusionScreen();
        }
    }
}
