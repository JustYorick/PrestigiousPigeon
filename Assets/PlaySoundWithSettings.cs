using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWithSettings : MonoBehaviour
{
    [SerializeField] private AudioClip sound;

    [SerializeField] private float soundVolMultiplier = 1f;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlaySoundWithVolumeMultiplier(sound, soundVolMultiplier);
    }
}
