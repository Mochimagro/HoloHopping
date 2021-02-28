using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using DG.Tweening;

namespace HoloHopping.Component
{

    public class ItemTextComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text = null;

        private Sequence _seq = null;

        public void Init(Entity.ItemGetEntity entity, Camera targetCamera)
        {
            _text.gameObject.SetActive(true);

            _text.text = entity.GetText;

            _text.color = entity.ItemColor;

            _text.rectTransform.position = RectTransformUtility.WorldToScreenPoint(targetCamera, entity.GetPosition + new Vector3(0, 1.0f, 0));

            _seq = DOTween.Sequence();

            _seq.Append(_text.rectTransform.DOMoveY(60f, 0.7f).SetRelative(true))
                .Append(_text.rectTransform.DOMoveY(100f, 0.25f).SetRelative(true))
                .Join(_text.rectTransform.DOScaleX(0, 0.25f));

            /*
            
            _text.DOFade(0, 0);
            DOTweenTMPAnimator tmpAnimator = new DOTweenTMPAnimator(_text);

            for(int i = 0;i < tmpAnimator.textInfo.characterCount; i++)
            {
                tmpAnimator.DOScaleChar(i, 0.45f, 0);
                Vector3 currCharOffset = tmpAnimator.GetCharOffset(i);

                _seq
                    .Join(tmpAnimator.DOOffsetChar(i, currCharOffset + new Vector3(0, 0.8f, 0), 0.15f).SetEase(Ease.OutFlash, 2))
                    .Join(tmpAnimator.DOFadeChar(i, 1, 0.05f))
                    .Join(tmpAnimator.DOScaleChar(i, 1, 0.4f).SetEase(Ease.OutBack))
                    .SetDelay(0.07f * i);

            }
            */

            _seq.Play();
            _seq.OnComplete(() =>
            {
                Destroy(gameObject);
                _seq.Kill();
            });
            //.SetDelay(0.7f);
        }

    }
}