using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace HoloHopping.OutGame.Component
{
    public class TitleTextTweenComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText = null;

        private Sequence _titleSeq = null;

        private float aaa = 1.0f;

        public void Init()
        {
            _titleSeq = DOTween.Sequence();

            _titleText.DOFade(0, 0);

            DOTweenTMPAnimator tmpAniamtor = new DOTweenTMPAnimator(_titleText);


            for (int i = 0; i < tmpAniamtor.textInfo.characterCount; i++)
            {

                var charSeq = DOTween.Sequence();

                tmpAniamtor.DOScaleChar(i, 0.7f, 0);

                Vector3 currentCharOffset = tmpAniamtor.GetCharOffset(i);

                charSeq
                    .Append(tmpAniamtor.DOOffsetChar(i, currentCharOffset + new Vector3(0, 60, 0), 0.4f * aaa).SetEase(Ease.OutFlash, 2))
                    .Join(tmpAniamtor.DOFadeChar(i, 1, 0.4f * aaa))
                    .Join(tmpAniamtor.DOScaleChar(i, 1, 0.4f * aaa).SetEase(Ease.OutBack))
                    .SetDelay(0.07f * i * aaa);

                _titleSeq.Join(charSeq);
            }

            _titleSeq.OnComplete(() =>
            {
                Doozy.Engine.GameEventMessage.SendEvent("TitleAniamtionComplete");
            });

            _titleSeq.Play();
        }

    }
}