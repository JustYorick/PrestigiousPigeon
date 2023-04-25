using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinesReader : MonoBehaviour
{
    private static LinesReader _instance;
    public static LinesReader Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        ReadCSV();
    }

    public TextAsset textAsset;
    public Canvas canvas;
    public RawImage characterPortrait;
    
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

    private void ReadCSV()
    {
        string[] data = textAsset.text.Split(new string[] { ";", "\n" }, System.StringSplitOptions.None);
        Debug.Log(""+data.Length);
        int tableSize = data.Length / 7 - 1;
        Debug.Log("tablesize: "+tableSize);

        for (int i = 0; i < tableSize; i++)
        {
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
