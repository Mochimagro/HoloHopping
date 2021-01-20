using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Component
{
    using Presenter;
    public class MainGameComponent : MonoBehaviour
    {
        [SerializeField] private HoppingCharaCreaterComponent _hoppingCharaCreaterComponent = null;
        [SerializeField] private ItemCreaterComponent _itemCreaterComponent = null;

        [SerializeField] private ScorePresenter _scorePresenter = null;

        private void Start()
        {
            Model.ScoreModel scoreModel = null;

            _scorePresenter.Init(out scoreModel);

            _hoppingCharaCreaterComponent.CreateHoppingCharacter();

            _itemCreaterComponent.Init(scoreModel);

            _itemCreaterComponent.StartAutoCreate();
        }

    }
}