using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Presenter
{
    using Model;
    using View;
    public class ItemCreaterPresenter : MonoBehaviour
    {
        [SerializeField] private ItemCreaterView _itemCreaterView = null;
        private ItemCreaterModel _itemCreaterModel = null;
        private ScoreModel _scoreModel = null;

        public Entity.ItemEntity NormalScoreItem = null;

        [SerializeField] private Data.ItemListData _itemListData = null;

        public IObservable<Entity.ItemGetEntity> OnGetItem => _itemCreaterView.OnGetItem;
        public IObservable<Component.ItemComponent> OnCreateItem => _onCreateItem;
        private Subject<Component.ItemComponent> _onCreateItem = new Subject<Component.ItemComponent>();

        public void Init(ScoreModel scoreModel)
        {
            _itemCreaterModel = new ItemCreaterModel(new Entity.ItemListEntity(_itemListData));
            _itemCreaterView.Init();
            _scoreModel = scoreModel;
            NormalScoreItem = _itemCreaterModel.NormalScoreItem;

            Bind();
        }

        private void Bind()
        {
            // アイテム獲得時の処理
            _itemCreaterView.OnGetItem.Subscribe(e =>
            {
                _scoreModel.AddScore = e.Score;

                _itemCreaterView.SendSpecialCreaterFSMTrigger(ArborFSMTrigger.ItemCreaterMessage.REDUCE_SPECIAL_ITEM_INTERVAL);

                _itemCreaterModel.RemoveFieldItem(e.TargetComponent);

            });


            // 個数0の時
            _itemCreaterModel.OnClearFieldItems.Subscribe(count =>
            {
                _scoreModel.AddScore = 5000;

                _itemCreaterView.ShowAllClearBonusPopup(5000);

                _itemCreaterView.SendNormalCreaterFSMTrigger(ArborFSMTrigger.ItemCreaterMessage.ALL_CLEAR_STAGE_ITEMS_CREATE);
            });

        }

        public Component.ItemComponent CreateSpecialItem(Vector3 barthPosition)
        {
            return CreateItem(_itemCreaterModel.GetSpecialItem, barthPosition);
        }

        public Component.ItemComponent CreateStarRushScoreItem(Vector3 barthPosition)
        {
            return CreateItem(_itemCreaterModel.GetStarRushItem, barthPosition);
        }

        public Component.ItemComponent CreateItem(Vector3 barthPosition)
        {
            var item = CreateItem(_itemCreaterModel.GetScoreItem, barthPosition);

            _itemCreaterModel.AddFieldItem(item);

            return item;
        }

        public Component.ItemComponent CreateFieldScoreItem(Vector3 barthPosition)
        {
            var item = CreateItem(_itemCreaterModel.NormalScoreItem, barthPosition);

            _itemCreaterModel.AddFieldItem(item);

            return item;
        }


        public Component.ItemComponent CreateItem(Entity.ItemEntity entity, Vector3 barthPosition)
        {
            var itemComponent = _itemCreaterView.CreateItem(entity, barthPosition);

            _onCreateItem.OnNext(itemComponent);

            return itemComponent;
        }

        public void GameStartCreate()
        {
            _itemCreaterView.SendNormalCreaterFSMTrigger(ArborFSMTrigger.ItemCreaterMessage.GAME_START_CREATE);
        }

    }
}