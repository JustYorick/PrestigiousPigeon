using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvent : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Scenes/Chapter 0/TutorialWithTerrainMap");
    }
}
