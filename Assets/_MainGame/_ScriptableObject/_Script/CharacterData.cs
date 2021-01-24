using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Enum
{
    public enum CharacterType
    {
        None,
        ControllPlayer,
        Hopping
    }
}

namespace HoloHopping.Entity
{
    public class CharacterEntity
    {
        public CharacterEntity(Data.CharacterData data)
        {
            CharacterComponent = data.CharacterObject;
            Name = data.Name;
        }

        public Component.CharacterComponent CharacterComponent { get; private set; }
        public string Name { get; private set; }

    }
}

namespace HoloHopping.Data
{

    [CreateAssetMenu(menuName =
        MenuName.Format.DATA +
        MenuName.Attribute.CHARACTER +
        MenuName.Type.PARAMETER,
        fileName = "NewCharacter")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private Component.CharacterComponent _characterObject = null;
        [SerializeField] private string _name = null;

        public Component.CharacterComponent CharacterObject { get { return _characterObject; } }
        public string Name { get { return _name; } }

    }
}