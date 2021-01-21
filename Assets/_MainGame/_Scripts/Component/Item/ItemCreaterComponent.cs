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
    }


    public class ItemCreaterComponent : MonoBehaviour
    {

        [SerializeField] private Arbor.ArborFSM _autoCreateState = null;
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

        public ItemComponent CreateItem(Data.ItemData itemData,Vector3 position)
        {

            var entity = new Entity.ItemEntity(itemData);
            var item = Instantiate(entity.Component,position,Quaternion.identity);

            item.Init(entity);

            item.OnGetItem.Subscribe(e =>
            {
                _scoreModel.AddScore = e.Score;
                _onGetItem.OnNext(e);
            });


            return item;
        }
    }
}