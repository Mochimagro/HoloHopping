using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Enum
{
    public enum FXType
    {
        None,
        Item,
        HopNormal,
        HopSuper,
        Miss
    }
}

namespace HoloHopping.Entity
{
    public class FXEntity
    {
        public FXEntity(Data.FXData data)
        {
            Component = data.EffectComponent;
            Type = data.Type;
        }

        public Component.EffectComponent Component { get; private set; }
        public Enum.FXType Type { get; private set; }

    }
}


namespace HoloHopping.Data
{
    using Component;

    [CreateAssetMenu(menuName = 
        MenuName.Format.DATA +
        MenuName.Attribute.FX+
        MenuName.Type.PARAMETER,
        fileName = "NewFXNameData")]
    public class FXData : ScriptableObject
    {
        [SerializeField] private EffectComponent _component = null;
        [SerializeField] private Enum.FXType _type = default;

        public EffectComponent EffectComponent { get { return _component; } }
        public Enum.FXType Type { get { return _type; } }

    }
}