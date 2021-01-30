using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace HoloHopping.Entity
{
    public class CharacterEntityList
    {
        public CharacterEntityList(Data.CharacterList list)
        {
            Entities = new List<CharacterEntity>();
            EntitiesDictionary = new Dictionary<string, CharacterEntity>();

            foreach (var data in list.List)
            {
                EntitiesDictionary.Add(data.Name, new CharacterEntity(data));
                Entities.Add(new CharacterEntity(data));
            }
        }

        public List<CharacterEntity> Entities { get; private set; }

        public Dictionary<string, CharacterEntity> EntitiesDictionary { get; }

        public CharacterEntity GetRondomCharacter { get { return Entities[UnityEngine.Random.Range(0, Entities.Count)]; } }


    }
}

namespace HoloHopping.Data
{
    [CreateAssetMenu(menuName =
        MenuName.Format.DATA +
        MenuName.Attribute.CHARACTER +
        MenuName.Type.LIST,
        fileName = "CharacterList")]
    public class CharacterList : ScriptableObject
    {
        [SerializeField] private List<CharacterData> _list = new List<CharacterData>();

        public List<CharacterData> List { get { return _list; } }
    }
}