using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using System;

public class TitleAnimation : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI titleText = null;
    [SerializeField] TextMeshProUGUI pressText = null;
    [SerializeField] GameObject helmet = null;
    [SerializeField] Transform helmetTo = null;
    AudioSource audioSource => GetComponent<AudioSource>();
    [SerializeField] AudioClip startJingle = null;

    void Start()
    {

        titleText.DOFade(0, 0);

        DOTweenTMPAnimator tmproAnimator = new DOTweenTMPAnimator(titleText);

        for (int i = 0; i < tmproAnimator.textInfo.characterCount; ++i)
        {
            tmproAnimator.DOScaleChar(i, 0.7f, 0);
            Vector3 currCharOffset = tmproAnimator.GetCharOffset(i);
            DOTween.Sequence()
                .Append(tmproAnimator.DOOffsetChar(i, currCharOffset + new Vector3(0, 60, 0), 0.4f).SetEase(Ease.OutFlash, 2))
                .Join(tmproAnimator.DOFadeChar(i, 1, 0.4f))
                .Join(tmproAnimator.DOScaleChar(i, 1, 0.4f).SetEase(Ease.OutBack))
                .SetDelay(0.07f * i)
                .OnComplete(() =>
                {
                    pressText.DOFade(0, 0);

                    pressText.DOFade(1f, 2.0f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.OutQuad);

                })
                ;
        }

        Observable.EveryUpdate()
            .Where(_ => Input.GetButtonDown("Jump"))
            .Subscribe(_ =>
            {
                audioSource.loop = false;
                audioSource.clip = startJingle;
                audioSource.Play();

                helmet.transform.DOMove(helmetTo.transform.position, 0.5f)
                .SetEase(Ease.InCubic)
                .OnComplete(() =>
                {
                    Observable.Timer(TimeSpan.FromSeconds(1))
                    .Subscribe(__ =>
                    {

                        SceneManager.LoadSceneAsync("Main");
                    });
                });

            }).AddTo(this);

    }
}
