using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign.Entities
{
    public class PlayerSounds  : MonoBehaviour
    {
        [SerializeField] private AudioClip iceSpellSound;
        [SerializeField] private AudioClip fireSpellSound;
        [SerializeField] private AudioClip walkingSound;
        [SerializeField] private AudioClip bookSound;

        public void PlayWalking()
        {
            SoundManager.Instance.PlaySound(walkingSound);
        }
    }
}