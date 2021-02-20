using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Doozy.Engine.UI;

namespace HoloHopping.Component
{
    public struct ItemCreaterMessage
    {
        public const string GAME_START_CREATE = "GameStartCreate";
        public const string AUTO_CREATE_ITEM = "AutoCreateItem";
        public const string STOP_CREATE_ITEM = "StopCreateItem";
        public const string CREATE_SPECIAL_ITEM = "CreateSpecialItem";
        public const string STOP_INTERVAL_SPECIAL_ITEM = "StopIntervalSpecialItem";
        public const string RESTART_INTERVAL_SPECIAL_ITEM = "RestartIntervalSpecialItem";
        public const string REDUCE_SPECIAL_ITEM_INTERVAL = "ReduceSpecialItemInterval";
        public const string START_SPECIAL_ITEM_INTERVAL = "StarSpecialItemInterval";
        public const string ALL_CLEAR_STAGE_ITEMS_CREATE = "AllClearStageItemsCreate";
    }


    public class ItemCreaterComponent : MonoBehaviour
    {

        [SerializeField] private Arbor.ArborFSM _autoCreateState = null;
        [SerializeField] private Arbor.ArborFSM _specialItemCreater = null;

        [SerializeField] private Data.ItemListData _itemListData = null;
        private Entity.ItemListEntity ItemListEntity => new Entity.ItemListEntity(_itemListData);

        private Model.ScoreModel _scoreModel = null;

        public IObservable<Entity.ItemEntity> OnGetItem => _onGetItem;
        private Subject<Entity.ItemEntity> _onGetItem = new Subject<Entity.ItemEntity>();

        private ReactiveCollection<ItemComponent> _normalFieldItems = null;
        private List<Entity.HighScoreItem> _highScoreItem = null;
        public Entity.ItemEntity GetNormalScoreItem => ItemListEntity.NormalScoreItem;

        public IObservable<int> OnClearFieldItems => _normalFieldItems.ObserveCountChanged().Where(count => count <= 0);

        public Entity.ItemEntity GetScoreItem
        {
            get
            {

                foreach (var target in _highScoreItem)
                {
                    if (_normalFieldItems.Count < target.BorderStageItemCount)
                    {
                        return target.Entity;
                    }
                }
                return GetNormalScoreItem;
            }
        }


        public void Init(Model.ScoreModel scoreModel)
        {
            _scoreModel = scoreModel;
            _normalFieldItems = new ReactiveCollection<ItemComponent>();
            _highScoreItem = ItemListEntity.HighScoreItems;

            OnClearFieldItems.Subscribe(_ =>
            {
                AllClearBonus();
            });

        }

        private void AllClearBonus()
        {
            var popup = UIPopupManager.GetPopup("AllClearBonus");

            UIPopupManager.ShowPopup(popup, popup.AddToPopupQueue, false);

            _scoreModel.AddScore = 5000;

            _autoCreateState.SendTrigger(ItemCreaterMessage.ALL_CLEAR_STAGE_ITEMS_CREATE);

        }


        public void StartAutoCreate()
        {
            _autoCreateState.SendTrigger(ItemCreaterMessage.AUTO_CREATE_ITEM);
        }

        public void GameStartCreate()
        {
            _autoCreateState.SendTrigger(ItemCreaterMessage.GAME_START_CREATE);
        }

        public ItemComponent CreateItem(Data.ItemData itemData, Vector3 position)
        {
            return CreateItem(new Entity.ItemEntity(itemData), position);
        }

        public ItemComponent CreateItem(Entity.ItemEntity itemEntity, Vector3 position)
        {
            var item = Instantiate(itemEntity.Component, position, Quaternion.identity);

            item.Init(itemEntity);

            item.OnGetItem.Subscribe(e =>
            {
                _normalFieldItems.Remove(item);
                _specialItemCreater.SendTrigger(ItemCreaterMessage.REDUCE_SPECIAL_ITEM_INTERVAL);
                e.FXCreateEntity = new Entity.FXCreateEntity(e.GetPos, Enum.FXType.Item, e.ItemColor);
                _scoreModel.AddScore = e.Score;
                _onGetItem.OnNext(e);
            });


            return item;
        }

        public void AddListItem(ItemComponent item)
        {
            _normalFieldItems.Add(item);

            item.OnDeathItem.Subscribe(e =>
            {
                _normalFieldItems.Remove(item);
            });
        }

        public void SendNormalCreaterStateTrigger(string message)
        {
            _autoCreateState.SendTrigger(message);
        }

        public void SendSpecialCreaterStateTrigger(string message)
        {
            _specialItemCreater.SendTrigger(message);
        }

        public void FieldItemEnable(bool value)
        {
            foreach (var item in _normalFieldItems)
            {
                item.gameObject.SetActive(value);
            }
        }

    }
}