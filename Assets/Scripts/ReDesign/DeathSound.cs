using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlaySound(deathSound);
    }
}
