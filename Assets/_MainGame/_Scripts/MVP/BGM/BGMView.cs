using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Collections;

namespace HoloHopping.View
{

    public class BGMView : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource = null;

        public IObservable<Unit> OnCompleteSound => _onCompleteSound;
        private Subject<Unit> _onCompleteSound;

        public void Init()
        {

        }

        public void StopSound()
        {
            _audioSource.Stop();
        }
        public void PlayLoopBGM(AudioClip clip)
        {
            _audioSource.loop = true;
            PlayAudio(clip);
        }

        /// <summary>
        /// 一発のサウンドを再生する
        /// </summary>
        /// <param name="clip"></param>
        public void PlaySimpleSound(AudioClip clip)
        {
            _audioSource.loop = false;
            PlayAudio(clip);
        }

        /// <summary>
        /// 一発のサウンドを再生する(コールバック付き)
        /// </summary>
        /// <param name="clip"></param>
        public void PlaySound(AudioClip clip)
        {
            PlaySimpleSound(clip);

            _onCompleteSound = new Subject<Unit>();
            StartCoroutine(CheckOnSoundEnd());
        }

        /// <summary>
        /// View内共通音声再生
        /// </summary>
        /// <param name="clip"></param>
        private void PlayAudio(AudioClip clip)
        {
            _audioSource.Stop();
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        private IEnumerator CheckOnSoundEnd()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();

                if (!_audioSource.isPlaying)
                {
                    _onCompleteSound.OnNext(Unit.Default);
                    _onCompleteSound.OnCompleted();
                    break;
                }
            }


        }

    }
}