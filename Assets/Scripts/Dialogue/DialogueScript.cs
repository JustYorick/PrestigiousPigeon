using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    private string text;
    [SerializeField] private float textSpeed;
    [SerializeField] private float clickCooldownAmount;
    private float cooldown;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = -1;
        //text = LinesReader.Instance.linesList.dialogueLines[index].dialogueLine;
        textComponent.text = string.Empty;
        NextLine();
        //StartDialogue();
        //index++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > cooldown + clickCooldownAmount)
        {
            cooldown = Time.time;
            Debug.Log("text: "+ text+";;;; index:"+index);
            if (textComponent.text.Equals(text))
            {
                NextLine();
            } 
            else
            {
                StopAllCoroutines();
                textComponent.text = text;
            }
        }
    }

    void StartDialogue()
    {
        StartCoroutine(TypeCharactersEffect());
        LoadImage();
        //Change background
        //Change portrait
        //Play music
        //Play sound effect
        //Play effects
    }

    IEnumerator TypeCharactersEffect()
    {
        text = LinesReader.Instance.linesList.dialogueLines[index].dialogueLine;
        foreach (char c in text.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < LinesReader.Instance.linesList.dialogueLines.Count - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartDialogue();
        } else
        {
            //gameObject.SetActive(false);
            LinesReader.Instance.canvas.gameObject.SetActive(false);
        }
    }

    void LoadImage()
    {
        MemoryStream dest = new MemoryStream();
        
        //Read from each Image File
        using (Stream source = File.OpenRead("Assets\\Images\\Character Portraits\\" + LinesReader.Instance.linesList.dialogueLines[index].speakingCharImg))
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
        LinesReader.Instance.characterPortrait.texture = tempTexture;
    }
}
