using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Entity
{
    public class CharacterEntity
    {
        public CharacterEntity(Data.CharacterData data)
        {
            CharacterObject = data.CharacterObject;
            Name = data.Name;
        }

        public GameObject CharacterObject { get; private set; }
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
        [SerializeField] private GameObject _characterObject = null;
        [SerializeField] private string _name = null;

        public GameObject CharacterObject { get { return _characterObject; } }
        public string Name { get { return _name; } }

    }
}