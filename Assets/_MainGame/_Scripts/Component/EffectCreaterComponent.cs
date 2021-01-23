using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Component
{

    public class EffectCreaterComponent : MonoBehaviour
    {
        [SerializeField] private Data.FXList _fxList = null;

        private Dictionary<Enum.FXType, EffectComponent> _fxDictionary = new Dictionary<Enum.FXType, EffectComponent>();

        public void Init()
        {
            var entity = new Entity.FXEntityList(_fxList);

            foreach(var item in entity.Entities)
            {
                _fxDictionary.Add(item.Type, item.Component);
            }

        }

        public void CreateEffect(Entity.FXCreateEntity entity)
        {
            CreateEffect(entity.Position, entity.FXType);
        }

        private void CreateEffect(Vector3 pos,Enum.FXType type)
        {
            var item = Instantiate(_fxDictionary[type], pos, Quaternion.identity);

        }

    }
}