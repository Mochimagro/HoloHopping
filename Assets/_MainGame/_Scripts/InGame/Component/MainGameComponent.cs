using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Doozy.Engine;
using Arbor;

namespace HoloHopping.Component
{
    using Presenter;
    using Model;
    public class MainGameComponent : MonoBehaviour
    {
        [Header("ArborFSM")]
        [SerializeField] private ArborFSM arborFSM = null;

        [Header("Components")]
        [SerializeField] private MainPlayerComponent _mainPlayerComponent = null;
        [SerializeField] private HoppingCharaCreaterComponent _hoppingCharaCreaterComponent = null;
        [SerializeField] private ItemCreaterComponent _itemCreaterComponent = null;
        [SerializeField] private ItemTextCreaterComponent _itemTextCreaterComponent = null;
        [SerializeField] private SpecialItemEffectComponent _specialItemEffectComponent = null;
        [SerializeField] private EffectCreaterComponent _effectCreaterComponent = null;

        [Header("Presenters")]
        [SerializeField] private ScorePresenter _scorePresenter = null;
        [SerializeField] private ReadyLabelPresenter _readyLabelPresenter = null;
        [SerializeField] private BGMPresenter _bgmPresenter = null;
        [SerializeField] private SEPresenter _sePresenter = null;
        [SerializeField] private RankingPresenter _rankingPresenter = null;

        [Header("Model")]
        private ScoreModel _scoreModel = null;

        private GameSystemModel _gameSystemModel = null;

        public void Init()
        {
            DG.Tweening.DOTween.SetTweensCapacity(500, 100);

            _mainPlayerComponent.DebugInit();

            _readyLabelPresenter.ShowReadyText();

            _gameSystemModel = new GameSystemModel();

            _scorePresenter.Init(out _scoreModel);

            _itemCreaterComponent.Init(_scoreModel);
            _specialItemEffectComponent.Init();

            _effectCreaterComponent.Init();

            _bgmPresenter.Init();
            _bgmPresenter.PlayReadySound();

            _sePresenter.Init();

            _rankingPresenter.Init(_scoreModel);

            Bind();
        }

        public void InvokeSystem()
        {
            GameEventMessage.SendEvent(NodyGameEventList.GAME_START);

            _readyLabelPresenter.ShowGoText();

            _itemCreaterComponent.GameStartCreate();

            _hoppingCharaCreaterComponent.CreateHoppingCharacter();

            _bgmPresenter.PlayStartSound();
        }

        private void Bind()
        {
            // GetItem
            _itemCreaterComponent.OnGetItem.Subscribe(entity =>
            {
                _specialItemEffectComponent.InvokeEffect(entity.ItemMode);
                _itemTextCreaterComponent.CreateText(entity);
                _sePresenter.PlaySound(entity.SEScene);
                _effectCreaterComponent.CreateEffect(entity.FXCreateEntity);
            });

            // CreatedHoppingCharacter
            _hoppingCharaCreaterComponent.OnCreateCharacter.Subscribe(createdChara =>
            {
                Debug.Log("Create");
                _gameSystemModel.AddLeaveCount();
            });

            // MissCharacter
            _hoppingCharaCreaterComponent.OnCharacterMiss.Subscribe(missChara =>
            {
                Debug.Log("Miss");
                _gameSystemModel.ReduceLeaveCount();
                _effectCreaterComponent.CreateEffect(missChara.FXCreateEntity);
            });

            // HoppedCharacter
            _hoppingCharaCreaterComponent.OnHopCharacter.Subscribe(fxEntity =>
            {
                _effectCreaterComponent.CreateEffect(fxEntity);
            });

            // AllMissed
            _gameSystemModel.AllCharacterMiss.Subscribe(_ =>
            {
                Debug.Log("AllMiss");
                arborFSM.SendTrigger(TriggerNameList.ALLMISS);
            });
        }

        public void GameOver()
        {
            GameEventMessage.SendEvent(NodyGameEventList.GAME_OVER);
            _bgmPresenter.PlayGameOverSound();
        }

    }
}