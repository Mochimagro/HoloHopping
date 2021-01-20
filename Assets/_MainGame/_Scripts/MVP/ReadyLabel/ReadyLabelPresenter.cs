using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Presenter
{
    using View;
    public class ReadyLabelPresenter : MonoBehaviour
    {
        [SerializeField] private ReadyLabelView _readyLabelView = null;

        private void Init()
        {

        }

        public void ShowReadyText()
        {
            _readyLabelView.SetLabelText = "READY...";
        }

        public void ShowGoText()
        {
            _readyLabelView.SetLabelText = "GO!!!";
        }


    }
}