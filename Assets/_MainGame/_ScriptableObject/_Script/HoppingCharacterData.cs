using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Data
{
    [CreateAssetMenu(menuName ="Data/HoppingCharacter",fileName ="NewCharacterName")]
    public class HoppingCharacterData : ScriptableObject
    {
        [SerializeField] private Component.HoppingCharacterComponent _prefab = null;

        public Component.HoppingCharacterComponent CharaComponent
        {
            get { return _prefab; }
        }

    }
}