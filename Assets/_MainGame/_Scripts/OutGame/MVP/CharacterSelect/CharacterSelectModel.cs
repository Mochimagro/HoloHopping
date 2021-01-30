using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Model
{
    public class CharacterSelectModel
    {
        private IntReactiveProperty _listIndex = null;
        private List<Entity.CharacterEntity> _characterList = null;


        public CharacterSelectModel(Entity.CharacterEntityList listEntity)
        {
            _listIndex = new IntReactiveProperty(0);
            _characterList = new List<Entity.CharacterEntity>();

            foreach (var e in listEntity.EntitiesDictionary)
            {
                _characterList.Add(e.Value);
            }


        }

        public IObservable<int> OnChangeIndex => _listIndex;

        public int SetIndexAdd
        {
            set { _listIndex.Value = (int)Mathf.Repeat(_listIndex.Value + value, _characterList.Count); }
        }

        public string GetSelectCharacterName
        {
            get { return _characterList[_listIndex.Value].Name; }
        }

    }
}