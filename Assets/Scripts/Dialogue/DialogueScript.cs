using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
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
    [SerializeField] private string nextSceneName;
    [SerializeField] private RawImage blackFade;
    [SerializeField] private GameObject skipPanel;

    private string text;
    [SerializeField] private float textSpeed;
    [SerializeField] private float clickCooldownAmount;
    private float cooldown;
    private int index;
    private bool lineComplete;
    private bool fadeFinished;
    private IEnumerator typeCoroutine;
    private bool receiveInput;

    [SerializeField] LinesReader linesReader;

    void Start()
    {
        SoundManager.Instance.SetMusic(null);

        receiveInput = true;
        skipPanel.SetActive(false);
        canvas.gameObject.SetActive(true);
        linesReader.ReadCSV();
        index = -1;
        textComponent.text = string.Empty;
        NextLine();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > cooldown + clickCooldownAmount && receiveInput)
        {
            cooldown = Time.time;
            if (lineComplete && fadeFinished)
            {
                NextLine();
            } 
            else if (fadeFinished)
            {
                // I think it works like this, but I'm not sure..
                StopCoroutine(typeCoroutine);
                //StopAllCoroutines();
                textComponent.text = linesReader.linesList.dialogueLines[index].dialogueLine;
                lineComplete = true;
            }
        }
    }

    void StartDialogue()
    {
        lineComplete = false;
        textComponent.text = linesReader.linesList.dialogueLines[index].dialogueLine;
        typeCoroutine = TypeCharactersEffect();
        StartCoroutine(typeCoroutine);

        if (linesReader.linesList.dialogueLines[index].speakingCharImg != string.Empty)
        {
            Texture2D newTexture = LoadImage("Assets\\Images\\Character Portraits\\", linesReader.linesList.dialogueLines[index].speakingCharImg);
            characterPortrait.texture = newTexture;
        } else
        {
            Texture2D newTexture = LoadImage("Assets\\Images\\Character Portraits\\", "emptycharacter.png");
            characterPortrait.texture = newTexture;
        }

        if (linesReader.linesList.dialogueLines[index].backgroundImg != string.Empty)
            backgroundImage.texture = LoadImage("Assets\\Images\\Backgrounds\\", linesReader.linesList.dialogueLines[index].backgroundImg);

        if (linesReader.linesList.dialogueLines[index].speakerName != string.Empty)
        {
            speakerNameText.text = linesReader.linesList.dialogueLines[index].speakerName;
        } else
        {
            speakerNameText.text = "";
        }
            

        
        if (linesReader.linesList.dialogueLines[index].music.Length > 0)
        {
            fadeFinished = false;
            StartCoroutine(FadeOutAudio());
        } else
        {
            fadeFinished = true;
        }

        if (linesReader.linesList.dialogueLines[index].soundEffect != string.Empty)
            StartCoroutine(LoadAudio("\\Sounds\\Effects\\" + linesReader.linesList.dialogueLines[index].soundEffect, soundEffect, false));

        //Play effects
    }

    private IEnumerator LoadAudio(string filePath, AudioSource audioSource, bool isMusic)
    {
        string path = "file:///" + Application.dataPath + filePath;
        UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG);
        yield return req.SendWebRequest();
        audioSource.clip = DownloadHandlerAudioClip.GetContent(req);
        if (isMusic)
        {
            audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        } else
        {
            audioSource.volume = PlayerPrefs.GetFloat("EffectVolume");
        }
        
        audioSource.Play();
        //SoundManager.Instance.SetMusic(audioSource.clip);
    }

    private IEnumerator FadeOutAudio()
    {
        while (backgroundMusic.volume > 0f)
        {
            backgroundMusic.volume -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        backgroundMusic.volume = 1f;
        if (linesReader.linesList.dialogueLines[index].music.ToLower().Equals("stopmusic"))
        {
            backgroundMusic.Stop();
        }
        else
        {
            StartCoroutine(LoadAudio("\\Sounds\\Music\\" + linesReader.linesList.dialogueLines[index].music, backgroundMusic, true));
        }
        fadeFinished = true;
    }

    private IEnumerator TypeCharactersEffect()
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

    private void NextLine()
    {
        if (index < linesReader.linesList.dialogueLines.Count - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartDialogue();
        } else
        {
            EndScene();
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

    private IEnumerator FadeToBlackAndNextScene()
    {
        float opacity = 0.0f;
        while (opacity <= 1.01f)
        {
            blackFade.color = new Color(0, 0, 0, opacity);
            opacity += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene(nextSceneName);
    }

    public void EndScene()
    {
        receiveInput = false;
        if (nextSceneName.Length > 0)
        {
            StartCoroutine(FadeToBlackAndNextScene());
        }
        else
        {
            canvas.gameObject.SetActive(false);
        }
    }

    public void SkipButton()
    {
        Time.timeScale = 0f;
        skipPanel.SetActive(true);
    }

    public void CancelSkipButton()
    {
        Time.timeScale = 1f;
        skipPanel.SetActive(false);
    }
}
