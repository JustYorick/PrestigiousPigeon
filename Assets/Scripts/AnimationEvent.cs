using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private AudioClip soundToPlay;
    // scene to load in built settings Like: Scenes/Chapter 0/TutorialWithTerrainMap
    [SerializeField] private string nextScene; 
    public void LoadScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void PlaySound()
    {
        // Play sound when it is assigned
        if (soundToPlay)
        {
            SoundManager.Instance.PlaySound(soundToPlay);
        }
    }
}
