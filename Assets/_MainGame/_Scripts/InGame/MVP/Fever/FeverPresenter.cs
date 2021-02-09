using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Doozy.Engine;

namespace HoloHopping.Presenter
{
    using Model;
    using View;
    public class FeverPresenter : MonoBehaviour
    {
        public struct FeverTrigger
        {
            public const string TIME_UP_FEVER = "TimeUpFever";
            public const string ALL_GET_ITEM = "AllGetItem";
        }

        [SerializeField] private FeverView _feverView = null;
        private FeverModel _feverModel = null;

        [SerializeField] private Arbor.ArborFSM _arborFSM = null;

        public void Init()
        {
            GameEventMessage.SendEvent("StartFever");

            _feverModel = new FeverModel();
            _feverView.Init();


            Bind();
        }

        private void Bind()
        {
            _feverModel.OnItemCount.Subscribe(value =>
            {
                _feverView.SetRestItemText = value;
            });

            _feverModel.OnItemCount.Where(value => value <= 0).Subscribe(value =>
            {
                _arborFSM.SendTrigger(FeverTrigger.ALL_GET_ITEM);
            });


        }

        public void AddItem(Component.ItemComponent item)
        {
            item.OnGetItem.Subscribe(_ =>
            {
                _feverModel.RemoveItem(item);
            });
            _feverModel.AddItem(item);
        }

        public void StartCountDown(float time)
        {
            _feverView.StartCountDown(time);

            _feverView.OnCompleteTime.Subscribe(_ =>
            {
                _arborFSM.SendTrigger(FeverTrigger.TIME_UP_FEVER);
            });
        }
        /// <summary>
        /// 終了処理
        /// アイテム全回収かタイムアップで終了
        /// </summary>
        public void FinishFever()
        {
            _feverModel.AllRemoveItem();
            GameEventMessage.SendEvent("EndFever");
        }
    }
}