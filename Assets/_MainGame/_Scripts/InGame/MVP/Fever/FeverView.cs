using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using DG.Tweening;

namespace HoloHopping.View
{

    public class FeverView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _restItemCount = null;
        [SerializeField] private Image _timeFillImage = null;
        private float _time = 0;

        private Sequence _countDownSequence = null;
        private Sequence _bonusScoreSequence = null;

        public IObservable<Unit> OnCompleteTime => _onCompleteTime;
        private Subject<Unit> _onCompleteTime = null;

        public IObservable<Unit> OnBonusTweenEnd => _onBonusTweenEnd;
        private Subject<Unit> _onBonusTweenEnd;

        public void Init()
        {
            _onCompleteTime = new Subject<Unit>();

            _restItemCount.alpha = 0.45f;

        }

        public void StartCountDown(float time)
        {
            _time = time;

            _timeFillImage.fillAmount = 1;

            _countDownSequence = DOTween.Sequence();

            _countDownSequence
                .Append(_timeFillImage.DOFillAmount(0, _time).SetEase(Ease.Linear))
                .OnStepComplete(() =>
                {
                    _countDownSequence.Kill();
                    _onCompleteTime.OnNext(Unit.Default);
                    _onCompleteTime.OnCompleted();
                });

            _countDownSequence.Play();

        }

        private Sequence SetBonusSequence(int score)
        {

            _restItemCount.text = "BONUS +" + score;

            _onBonusTweenEnd = new Subject<Unit>();

            var seq = DOTween.Sequence();

            seq.Append(_restItemCount.DOFade(0, 0));

            var tmproAnimator = new DOTweenTMPAnimator(_restItemCount);

            for (int i = 0; i < tmproAnimator.textInfo.characterCount; i++)
            {
                Vector3 currCharOffset = tmproAnimator.GetCharOffset(i);

                var charSeq = DOTween.Sequence();

                charSeq.Append(tmproAnimator.DOFadeChar(i, 0.45f, 0.45f));
                charSeq.Join(tmproAnimator.DOOffsetChar(i, currCharOffset + new Vector3(0, 30f, 0), 0.25f).SetEase(Ease.OutFlash, 2));

                seq.Join(charSeq.SetDelay(i * 0.1f));
            }

            seq.OnComplete(() =>
            {
                _onBonusTweenEnd.OnNext(Unit.Default);
                _onBonusTweenEnd.OnCompleted();
            });

            return seq;
        }

        public void PlayBonusText(int score)
        {
            _bonusScoreSequence = SetBonusSequence(score);

            _bonusScoreSequence.Play();
            _countDownSequence.Pause();

        }

        public void KillSequence()
        {
            _bonusScoreSequence.Kill();
            _countDownSequence.Kill();
        }

        public int SetRestItemText { set { _restItemCount.text = value.ToString(); } }

        public float SetTimeIntervalFillAmount { set { _timeFillImage.fillAmount = value; } }
    }
}