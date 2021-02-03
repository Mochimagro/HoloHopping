using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Doozy.Engine.UI;
using InControl;

namespace HoloHopping.View
{
    public class CharacterSelectView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCharacterName = null;
        [SerializeField] private UIButton _buttonLeft = null;
        [SerializeField] private UIButton _buttonRight = null;
        [SerializeField] private UIButton _buttonDecision = null;

        private Subject<Unit> _onSelectLeft = new Subject<Unit>();
        private Subject<Unit> _onSelectRight = new Subject<Unit>();
        private Subject<Unit> _onSelectDecision = new Subject<Unit>();

        private InputDevice _activeDevice = null;

        public IObservable<Unit> OnSelectLeft => _onSelectLeft;
        public IObservable<Unit> OnSelectRight => _onSelectRight;
        public IObservable<Unit> OnDecision => _onSelectDecision;

        public void Init()
        {
            _activeDevice = InputManager.ActiveDevice;

            _buttonLeft.OnClick.OnTrigger.Action += _ => _onSelectLeft.OnNext(Unit.Default);
            _buttonRight.OnClick.OnTrigger.Action += _ => _onSelectRight.OnNext(Unit.Default);
            _buttonDecision.OnClick.OnTrigger.Action += _ => _onSelectDecision.OnNext(Unit.Default);

            Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.RightArrow)).Subscribe(_ =>
            {
                _buttonRight.OnClick.PlayAnimation(_buttonRight);
                _buttonRight.OnClick.OnTrigger.InvokeAction(_buttonRight.gameObject);
            });

            Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.LeftArrow)).Subscribe(_ =>
            {
                _buttonLeft.OnClick.PlayAnimation(_buttonLeft);
                _buttonLeft.OnClick.OnTrigger.InvokeAction(_buttonLeft.gameObject);
            });

            Observable.EveryUpdate().Where(_ => Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump")).Subscribe(_ =>
            {
                _buttonDecision.OnClick.PlayAnimation(_buttonDecision);
                _buttonDecision.OnClick.OnTrigger.InvokeAction(_buttonDecision.gameObject);
            });

        }

        public string SetTextCharacterName
        {
            set { _textCharacterName.text = value; }
        }

    }
}