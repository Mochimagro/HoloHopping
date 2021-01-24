using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Presenter
{
    using Model;
    using View;
    public class SEPresenter : MonoBehaviour
    {
        [SerializeField] private SEView _seView = null;
        private SEModel _seModel = null;

        [SerializeField] private Data.SEList _seList = null;

        public void Init()
        {
            _seModel = new SEModel(new Entity.SEEntityList(_seList));
            _seView.StopSound();
        }

        public void PlaySound(Enum.SEScene seScene)
        {
            _seView.ShotOneSound(_seModel.GetSEClip(seScene));
        }
    }
}