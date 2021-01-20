using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Data
{
    [CreateAssetMenu(menuName =
        MenuName.Format.DATA + 
        MenuName.Attribute.HOPPING_CHARACTER + 
        MenuName.Type.PARAMETER,
        fileName ="NewCharacterName")]
    public class HoppingCharacterData : ScriptableObject
    {
        [SerializeField] private Component.HoppingCharacterComponent _prefab = null;

        public Component.HoppingCharacterComponent CharaComponent
        {
            get { return _prefab; }
        }

    }
}