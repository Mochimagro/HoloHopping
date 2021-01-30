using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Doozy.Engine.UI;

namespace HoloHopping.View
{
    public class CharacterSelectView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCharacterName = null;
        [SerializeField] private UIButton _buttonLeft = null;
        [SerializeField] private UIButton _buttonRight = null;
        [SerializeField] private UIButton _buttonDecision = null;

        public IObservable<Unit> OnSelectLeft => _buttonLeft.Button.OnClickAsObservable();
        public IObservable<Unit> OnSelectRight => _buttonRight.Button.OnClickAsObservable();
        public IObservable<Unit> OnDecision => _buttonDecision.Button.OnClickAsObservable();

        public void Init()
        {


        }

        public string SetTextCharacterName
        {
            set { _textCharacterName.text = value; }
        }

    }
}