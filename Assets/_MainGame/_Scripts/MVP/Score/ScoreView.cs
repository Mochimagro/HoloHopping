using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace HoloHopping.View
{

    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textScore = null;
        private int _prevValue = 0;

        public int SetScoreText
        {
            set 
            {
                int i = _prevValue;
                _prevValue = value;
                DOTween.To(
                    () => i,
                    num =>
                    {
                        i = num;
                        _textScore.text = i.ToString();
                    },
                    _prevValue,
                    0.5f
                    ).SetEase(Ease.OutQuad);
            }
        }

        public void Init()
        {
            _textScore.text = string.Format("{0}", 0);
        }

    }
}