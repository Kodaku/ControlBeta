using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueReader
{
    public TextAsset[] jsonFiles;
    public SimpleDialogue[] ReadDialogues(int fileIndex)
    {
        Dialogues dialoguesInJson = JsonUtility.FromJson<Dialogues>(jsonFiles[fileIndex].text);
        
        return dialoguesInJson.dialogues;
    }
}
