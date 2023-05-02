using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RawImage characterPortrait;
    [SerializeField] private RawImage backgroundImage;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource soundEffect;

    private string text;
    [SerializeField] private float textSpeed;
    [SerializeField] private float clickCooldownAmount;
    private float cooldown;
    private int index;
    private bool lineComplete;

    [SerializeField] LinesReader linesReader;

    void Start()
    {
        linesReader.ReadCSV();
        index = -1;
        //text = LinesReader.Instance.linesList.dialogueLines[index].dialogueLine;
        textComponent.text = string.Empty;
        NextLine();
        //StartDialogue();
        //index++;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > cooldown + clickCooldownAmount)
        {
            cooldown = Time.time;
            Debug.Log("text: "+ text+";;;; index:"+index);
            if (lineComplete)
            {
                NextLine();
            } 
            else
            {
                StopAllCoroutines();
                textComponent.text = linesReader.linesList.dialogueLines[index].dialogueLine;
                lineComplete = true;
            }
        }
    }

    void StartDialogue()
    {
        lineComplete = false;
        textComponent.text = linesReader.linesList.dialogueLines[index].dialogueLine;
        StartCoroutine(TypeCharactersEffect());

        if (linesReader.linesList.dialogueLines[index].speakingCharImg != string.Empty)
            characterPortrait.texture = LoadImage("Assets\\Images\\Character Portraits\\", linesReader.linesList.dialogueLines[index].speakingCharImg);

        if (linesReader.linesList.dialogueLines[index].backgroundImg != string.Empty)
            backgroundImage.texture = LoadImage("Assets\\Images\\Backgrounds\\", linesReader.linesList.dialogueLines[index].backgroundImg);

        if (linesReader.linesList.dialogueLines[index].speakerName != string.Empty)
            speakerNameText.text = linesReader.linesList.dialogueLines[index].speakerName;

        if (linesReader.linesList.dialogueLines[index].music != string.Empty)
            StartCoroutine(LoadAudio("\\Sounds\\Music\\" + linesReader.linesList.dialogueLines[index].music, backgroundMusic));

        if (linesReader.linesList.dialogueLines[index].soundEffect != string.Empty)
            StartCoroutine(LoadAudio("\\Sounds\\Effects\\" + linesReader.linesList.dialogueLines[index].soundEffect, soundEffect));


        //Change background
        //Change portrait
        //Play music
        //Play sound effect
        //Play effects
    }

    private IEnumerator LoadAudio(string filePath, AudioSource audioSource)
    { 
        string path = "file:///" + Application.dataPath + filePath;
        UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG);
        yield return req.SendWebRequest();
        audioSource.clip = DownloadHandlerAudioClip.GetContent(req);
        audioSource.Play();
    }

    IEnumerator TypeCharactersEffect()
    {
        string textToWrite = linesReader.linesList.dialogueLines[index].dialogueLine;
        string colorTag = "<color=#00000000>";

        int index2 = 0;
        while (index2 <= textToWrite.Length)
        {
            textComponent.text = textToWrite.Substring(0, index2) + colorTag + textToWrite.Substring(index2) + "</color>";
            index2++;
            yield return new WaitForSeconds(textSpeed);
        }

        lineComplete = true;
    }

    void NextLine()
    {
        if (index < linesReader.linesList.dialogueLines.Count - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartDialogue();
        } else
        {
            //gameObject.SetActive(false);
            canvas.gameObject.SetActive(false);
            //GO NEXT SCENE
        }
    }

    private Texture2D LoadImage(string folderPath, string imgName)
    {
        MemoryStream dest = new MemoryStream();
        
        //Read from each Image File
        using (Stream source = File.OpenRead(folderPath + imgName))
        {
            byte[] buffer = new byte[2048];
            int bytesRead;
            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                dest.Write(buffer, 0, bytesRead);
            }
        }
        byte[] imageBytes = dest.ToArray();

        //Create new Texture2D
        Texture2D tempTexture = new Texture2D(2, 2);

        //Load the Image Byte to Texture2D
        tempTexture.LoadImage(imageBytes);
        return tempTexture;
    }
}
