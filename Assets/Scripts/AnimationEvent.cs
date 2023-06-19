using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private AudioClip soundToPlay; 
    public void LoadScene()
    {
        SceneManager.LoadScene("Scenes/Chapter 0/TutorialWithTerrainMap");
    }

    public void PlaySound()
    {
        // Play sound when it is assigned
        if (soundToPlay)
        {
            Debug.Log($"Playing {soundToPlay}");
            SoundManager.Instance.PlaySound(soundToPlay);
        }
    }
}
