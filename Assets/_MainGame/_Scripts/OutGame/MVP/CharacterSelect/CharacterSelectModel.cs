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
        private List<GameObject> _characterObjectList = null;
        private GameObject _selectCharacterObject = null;

        public CharacterSelectModel(Entity.CharacterEntityList listEntity)
        {
            _listIndex = new IntReactiveProperty(0);
            _characterList = new List<Entity.CharacterEntity>();
            _characterObjectList = new List<GameObject>();

            foreach (var entity in listEntity.EntitiesDictionary)
            {
                var e = entity;

                e.Value.CharacterComponent.HoppingObjectSetActive = false;
                var chara = UnityEngine.Object.Instantiate(e.Value.CharacterComponent.gameObject);
                chara.SetActive(false);

                _characterObjectList.Add(chara);
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

        public GameObject GetSelectCharacterObject
        {
            get
            {
                if (_selectCharacterObject != null) _selectCharacterObject.SetActive(false);
                _selectCharacterObject = _characterObjectList[_listIndex.Value];
                return _selectCharacterObject;
            }
        }

    }
}