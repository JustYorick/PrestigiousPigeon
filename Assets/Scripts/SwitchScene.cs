using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    // Load the scene using the name of the scene
    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
}
