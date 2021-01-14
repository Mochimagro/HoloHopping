using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Legacy
{
    public class TextGetScore : MonoBehaviour
    {
        private TextMeshPro tmpro => GetComponent<TextMeshPro>();

        public void StartTextAnimation(string info, Color textColor)
        {

            tmpro.text = info;
            tmpro.color = textColor;
            tmpro.DOFade(0, 0);
            DOTweenTMPAnimator tmpAnimator = new DOTweenTMPAnimator(tmpro);

            for (int i = 0; i < tmpAnimator.textInfo.characterCount; i++)
            {
                tmpAnimator.DOScaleChar(i, 0.45f, 0);
                Vector3 currCharOffset = tmpAnimator.GetCharOffset(i);
                DOTween.Sequence()
                .Append(tmpAnimator.DOOffsetChar(i, currCharOffset + new Vector3(0, 0.8f, 0), 0.15f).SetEase(Ease.OutFlash, 2))
                //.Append(tmpAnimator.DOOffsetChar(i, currCharOffset + new Vector3(0, -0.4f, 0), 0.25f).SetEase(Ease.OutBounce))
                .Join(tmpAnimator.DOFadeChar(i, 1, 0.05f))
                .Join(tmpAnimator.DOScaleChar(i, 1, 0.4f).SetEase(Ease.OutBack))
                .SetDelay(0.07f * i)
                .OnComplete(() => { StartCoroutine(DestroyObject(tmpAnimator.textInfo.characterCount * 0.07f + 0.7f)); });
            }
        }

        public void StartTextAnimation(int score, Color textColor)
        {
            StartTextAnimation("+" + score, textColor);
        }


        private IEnumerator DestroyObject(float life)
        {
            yield return new WaitForSeconds(life);
            Destroy(this.gameObject);
        }
    }
}