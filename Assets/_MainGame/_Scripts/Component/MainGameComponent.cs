using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Doozy.Engine;

namespace HoloHopping.Component
{
    using Presenter;
    using Model;
    public class MainGameComponent : MonoBehaviour
    {
        [SerializeField] private HoppingCharaCreaterComponent _hoppingCharaCreaterComponent = null;
        [SerializeField] private ItemCreaterComponent _itemCreaterComponent = null;

        [SerializeField] private ScorePresenter _scorePresenter = null;
        [SerializeField] private ReadyLabelPresenter _readyLabelPresenter = null;
        private GameSystemModel _gameSystemModel = null;

        public void Init()
        {
            _readyLabelPresenter.ShowReadyText();

            ScoreModel scoreModel = null;
            _gameSystemModel = new GameSystemModel();

            _scorePresenter.Init(out scoreModel);

            _itemCreaterComponent.Init(scoreModel);

            Bind();
        }

        public void InvokeSystem()
        {
            GameEventMessage.SendEvent("StartGame");

            _readyLabelPresenter.ShowGoText();

            _itemCreaterComponent.StartAutoCreate();

            _hoppingCharaCreaterComponent.CreateHoppingCharacter();
        }

        private void Bind()
        {
            _hoppingCharaCreaterComponent.OnCreateCharacter.Subscribe(createdChara =>
            {
                Debug.Log("Create");
                _gameSystemModel.AddLeaveCount();
            });

            _hoppingCharaCreaterComponent.OnCharacterMiss.Subscribe(missChara =>
            {
                Debug.Log("Miss");
                _gameSystemModel.ReduceLeaveCount();
            });

            _gameSystemModel.AllCharacterMiss.Subscribe(_ =>
            {
                Debug.Log("AllMiss");
            });
        }

    }
}