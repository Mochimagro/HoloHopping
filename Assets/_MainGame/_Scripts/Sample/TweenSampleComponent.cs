using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

namespace HoloHopping.Sample.Component
{
    public class TweenSampleComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _targetText = null;
        [SerializeField] private Button _playButton = null;
        [SerializeField] private AnimationCurve _tweenCurve = null;

        private Sequence _onceSeq = null;
        private Sequence _loopSeq = null;

        public void Start()
        {
            _onceSeq = FeverBonusScoreTextTween();
            // _loopSeq = FeverColorfulTextTewwn();

            _playButton.OnClickAsObservable().Subscribe(_ =>
            {
                _onceSeq.Restart();
                // _loopSeq.Restart();
            });

        }

        private Sequence FeverBonusScoreTextTween()
        {
            var seq = DOTween.Sequence();

            seq.Append(_targetText.DOFade(0, 0));

            var tmproAnimator = new DOTweenTMPAnimator(_targetText);

            for (int i = 0; i < tmproAnimator.textInfo.characterCount; i++)
            {
                Vector3 currCharOffset = tmproAnimator.GetCharOffset(i);

                var charSeq = DOTween.Sequence();

                charSeq.Append(tmproAnimator.DOFadeChar(i, 1, 0.45f));
                charSeq.Join(tmproAnimator.DOOffsetChar(i, currCharOffset + new Vector3(0, 30f, 0), 0.25f).SetEase(Ease.OutFlash, 2));

                seq.Join(charSeq.SetDelay(i * 0.1f));
            }

            seq.OnComplete(() =>
            {
                Debug.Log("OnComplete");
            });

            return seq;
        }

        private Sequence FeverColorfulTextTewwn()
        {
            var seq = DOTween.Sequence();

            var tmproAnimator = new DOTweenTMPAnimator(_targetText);

            for (int i = 0; i < tmproAnimator.textInfo.characterCount; i++)
            {
                Vector3 currCharOffset = tmproAnimator.GetCharOffset(i);

                var charSeq = DOTween.Sequence();

                charSeq.Append(tmproAnimator.DOColorChar(i, Color.red, 0.1f));
                charSeq.Append(tmproAnimator.DOColorChar(i, Color.yellow, 0.1f));
                charSeq.Append(tmproAnimator.DOColorChar(i, Color.green, 0.1f));
                charSeq.Append(tmproAnimator.DOColorChar(i, Color.blue, 0.1f));
                charSeq.Append(tmproAnimator.DOColorChar(i, Color.magenta, 0.1f));

                charSeq.SetRelative().SetLoops(100);

                seq.Join(charSeq.SetDelay(i * 0.1f));
            }

            return seq;
        }

    }
}