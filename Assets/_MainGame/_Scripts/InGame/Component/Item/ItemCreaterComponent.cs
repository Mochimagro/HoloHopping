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

        public void Init(Model.ScoreModel scoreModel)
        {
            _scoreModel = scoreModel;
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
                _specialItemCreater.SendTrigger(ItemCreaterMessage.REDUCE_SPECIAL_ITEM_INTERVAL);
                e.FXCreateEntity = new Entity.FXCreateEntity(e.GetPos, Enum.FXType.Item, e.ItemColor);
                _scoreModel.AddScore = e.Score;
                _onGetItem.OnNext(e);
            });


            return item;
        }
    }
}