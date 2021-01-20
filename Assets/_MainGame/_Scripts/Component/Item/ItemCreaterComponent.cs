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
        // TODO:アイテムデータリストから取得する
        [SerializeField] private Data.ItemData _debudItem = null;

        [SerializeField] private Arbor.ArborFSM _autoCreateState = null;
        private Model.ScoreModel _scoreModel = null;

        public void Init(Model.ScoreModel scoreModel)
        {
            _scoreModel = scoreModel;
        }

        public void StartAutoCreate()
        {
            _autoCreateState.SendTrigger(ItemCreaterMessage.AUTO_CREATE_ITEM);
        }

        public ItemComponent CreateItem(Vector3 position)
        {

            var entity = new Entity.ItemEntity(_debudItem);
            var item = Instantiate(entity.Component,position,Quaternion.identity);

            item.Init(entity);

            item.OnGetItem.Subscribe(score =>
            {
                _scoreModel.AddScore = score;
            });


            return item;
        }
    }
}