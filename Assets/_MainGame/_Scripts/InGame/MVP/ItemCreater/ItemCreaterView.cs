using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Doozy.Engine.UI;

namespace HoloHopping.ArborFSMTrigger
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
}

namespace HoloHopping.View
{
    public class ItemCreaterView : MonoBehaviour
    {
        [SerializeField] private Arbor.ArborFSM _autoCreaterFSM = null;
        [SerializeField] private Arbor.ArborFSM _specialITemCreaterFSM = null;

        public IObservable<Entity.ItemGetEntity> OnGetItem => _onGetItem;
        private Subject<Entity.ItemGetEntity> _onGetItem = new Subject<Entity.ItemGetEntity>();

        public void Init()
        {


        }

        public Component.ItemComponent CreateItem(Entity.ItemEntity itemEntity, Vector3 barthPosition)
        {
            var item = Instantiate(itemEntity.Component, barthPosition, Quaternion.identity);

            item.Init(itemEntity);

            item.OnGetItem.Subscribe(e =>
           {
               e.FXCreateEntity = new Entity.FXCreateEntity(e.GetPosition, Enum.FXType.Item, e.ItemColor);
               _onGetItem.OnNext(e);

           });

            return item;
        }

        public void SendNormalCreaterFSMTrigger(string message)
        {
            _autoCreaterFSM.SendTrigger(message);
        }

        public void SendSpecialCreaterFSMTrigger(string message)
        {
            _specialITemCreaterFSM.SendTrigger(message);
        }

        public void FieldItemEnable(bool value, List<GameObject> fieldScoreItemObject)
        {
            foreach (var item in fieldScoreItemObject)
            {
                item.SetActive(value);
            }
        }

        public void ShowAllClearBonusPopup(int score)
        {
            var popup = UIPopupManager.GetPopup("AllClearBonus");

            UIPopupManager.ShowPopup(popup, popup.AddToPopupQueue, false);

            _autoCreaterFSM.SendTrigger(ArborFSMTrigger.ItemCreaterMessage.ALL_CLEAR_STAGE_ITEMS_CREATE);

        }

    }
}