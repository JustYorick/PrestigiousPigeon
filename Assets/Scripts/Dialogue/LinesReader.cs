using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinesReader : MonoBehaviour
{
    public TextAsset textAsset;
    
    [System.Serializable]
    public class DialogueLine
    {
        public string speakerName;
        public string dialogueLine;
        public string speakingCharImg;
        public string backgroundImg;
        public string soundEffect;
        public string music;
        public string effectAction; //Conceptual
    }

    [System.Serializable]
    public class LinesList
    {
        public List<DialogueLine> dialogueLines;
    }

    public LinesList linesList = new LinesList();

    public void ReadCSV()
    {
        string[] data = textAsset.text.Split(new string[] { ";", "\n" }, System.StringSplitOptions.None);
        int tableSize = data.Length / 7 - 1;

        for (int i = 0; i < tableSize; i++)
        {
            //This kind of sucks but it works so who cares.
            linesList.dialogueLines.Add(new DialogueLine());
            linesList.dialogueLines[i].speakerName = data[7 * (i + 1)];
            linesList.dialogueLines[i].dialogueLine = data[7 * (i + 1) + 1];
            linesList.dialogueLines[i].speakingCharImg = data[7 * (i + 1) + 2];
            linesList.dialogueLines[i].backgroundImg = data[7 * (i + 1) + 3];
            linesList.dialogueLines[i].soundEffect = data[7 * (i + 1) + 4];
            linesList.dialogueLines[i].music = data[7 * (i + 1) + 5];
            linesList.dialogueLines[i].effectAction = data[7 * (i + 1) + 6];
        }
    }
}
