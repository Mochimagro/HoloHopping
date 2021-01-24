using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.View
{

    public class SEView : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource = null;

        public void Init()
        {
            _audioSource.Stop();
        }

        public void StopSound()
        {
            _audioSource.Stop();
        }

        public void ShotOneSound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

    }
}