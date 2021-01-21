using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Component
{

    public class ItemTextCreaterComponent : MonoBehaviour
    {
        [SerializeField] private ItemTextComponent _itemTextComponent = null;
        [SerializeField] private RectTransform _parentCanvas = null;
        [SerializeField] private Camera _targetCamera = null;

        public void Init()
        {
            
        }

        public void CreateText(Entity .ItemEntity entity)
        {
            var text = Instantiate(_itemTextComponent, _parentCanvas);

            text.Init(entity,  _targetCamera);

        }
    }
}