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

        private Subject<Unit> _onCompleteTime = null;
        public IObservable<Unit> OnCompleteTime => _onCompleteTime;

        public void Init()
        {
            _onCompleteTime = new Subject<Unit>();

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
                    _onCompleteTime.OnNext(Unit.Default);
                    _onCompleteTime.OnCompleted();
                });

            _countDownSequence.Play();

        }


        public int SetRestItemText { set { _restItemCount.text = value.ToString(); } }

        public float SetTimeIntervalFillAmount { set { _timeFillImage.fillAmount = value; } }


    }
}