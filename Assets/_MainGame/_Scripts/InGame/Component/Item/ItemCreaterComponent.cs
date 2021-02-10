using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Component
{
    public struct ItemCreaterMessage
    {
        public const string AUTO_CREATE_ITEM = "AutoCreateItem";
        public const string STOP_CREATE_ITEM = "StopCreateItem";
        public const string CREATE_SPECIAL_ITEM = "CreateSpecialItem";
        public const string STOP_INTERVAL_SPECIAL_ITEM = "StopIntervalSpecialItem";
        public const string RESTART_INTERVAL_SPECIAL_ITEM = "RestartIntervalSpecialItem";
        public const string REDUCE_SPECIAL_ITEM_INTERVAL = "ReduceSpecialItemInterval";
        public const string START_SPECIAL_ITEM_INTERVAL = "StarSpecialItemInterval";
    }


    public class ItemCreaterComponent : MonoBehaviour
    {

        [SerializeField] private Arbor.ArborFSM _autoCreateState = null;
        [SerializeField] private Arbor.ArborFSM _specialItemCreater = null;
        private Model.ScoreModel _scoreModel = null;

        public IObservable<Entity.ItemEntity> OnGetItem => _onGetItem;
        private Subject<Entity.ItemEntity> _onGetItem = new Subject<Entity.ItemEntity>();

        private List<ItemComponent> _normalFieldItems = null;

        public void Init(Model.ScoreModel scoreModel)
        {
            _scoreModel = scoreModel;
            _normalFieldItems = new List<ItemComponent>();
        }

        public void StartAutoCreate()
        {
            _autoCreateState.SendTrigger(ItemCreaterMessage.AUTO_CREATE_ITEM);
        }

        public ItemComponent CreateItem(Data.ItemData itemData, Vector3 position)
        {

            var entity = new Entity.ItemEntity(itemData);
            var item = Instantiate(entity.Component, position, Quaternion.identity);

            item.Init(entity);

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