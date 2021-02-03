using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UniRx.Triggers;
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

        [SerializeField] private Transform _characterViewPosition = null;

        [SerializeField] private Arbor.ArborFSM _characterMoverFSM = null;

        private Subject<Unit> _onSelectLeft;
        private Subject<Unit> _onSelectRight;
        private Subject<Unit> _onSelectDecision;
        private Subject<Unit> _update;

        private InputDevice _activeDevice = null;

        public IObservable<Unit> OnSelectLeft => _onSelectLeft;
        public IObservable<Unit> OnSelectRight => _onSelectRight;
        public IObservable<Unit> OnDecision => _onSelectDecision;

        public void Init()
        {
            SetControllKey();
        }

        private void SetControllKey()
        {

            _activeDevice = InputManager.ActiveDevice;

            _onSelectLeft = new Subject<Unit>();
            _onSelectRight = new Subject<Unit>();
            _onSelectDecision = new Subject<Unit>();

            _buttonLeft.OnClick.OnTrigger.Action += _ => _onSelectLeft.OnNext(Unit.Default);
            _buttonRight.OnClick.OnTrigger.Action += _ => _onSelectRight.OnNext(Unit.Default);
            _buttonDecision.OnClick.OnTrigger.Action += _ => _onSelectDecision.OnNext(Unit.Default);


            this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.RightArrow)).TakeUntilDisable(this).Subscribe(_ =>
            {
                _buttonRight.OnClick.PlayAnimation(_buttonRight);
                _buttonRight.OnClick.OnTrigger.InvokeAction(_buttonRight.gameObject);
            });

            this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.LeftArrow)).TakeUntilDisable(this).Subscribe(_ =>
            {
                _buttonLeft.OnClick.PlayAnimation(_buttonLeft);
                _buttonLeft.OnClick.OnTrigger.InvokeAction(_buttonLeft.gameObject);
            });

            this.UpdateAsObservable().Where(_ => Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump")).TakeUntilDisable(this).Subscribe(_ =>
            {
                _buttonDecision.OnClick.PlayAnimation(_buttonDecision);
                _buttonDecision.OnClick.OnTrigger.InvokeAction(_buttonDecision.gameObject);

                _onSelectLeft.OnCompleted();
                _onSelectRight.OnCompleted();
                _onSelectDecision.OnCompleted();

            });

        }

        public string SetTextCharacterName
        {
            set { _textCharacterName.text = value; }
        }

        public void ShowSelectCharacterObject(GameObject selectCharacter)
        {
            selectCharacter.SetActive(true);
            selectCharacter.transform.SetParent(_characterViewPosition, false);
        }

        public void MoveSelectCharacter()
        {
            _characterMoverFSM.Play();
        }

    }
}