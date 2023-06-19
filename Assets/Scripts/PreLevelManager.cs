using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLevelManager : MonoBehaviour
{
    [SerializeField] private AudioClip levelMusic;

    [SerializeField] private AudioClip levelSound;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.SetMusic(levelMusic);
        SoundManager.Instance.PlaySound(levelSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
