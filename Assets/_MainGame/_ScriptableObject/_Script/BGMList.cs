using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace HoloHopping.Entity
{
    public class BGMEntityList
    {
        public BGMEntityList(Data.BGMList list)
        {
            Entities = new List<BGMEntity>();
            foreach (var data in list.BGMs)
            {
                Entities.Add(new BGMEntity(data));
            }
        }

        public List<BGMEntity> Entities { get; private set; }

    }
}

namespace HoloHopping.Data
{

    [CreateAssetMenu(menuName =
        MenuName.Format.DATA +
        MenuName.Attribute.BGM +
        MenuName.Type.LIST,
        fileName = "BGMList")]
    public class BGMList : ScriptableObject
    {
        [SerializeField] private List<BGMData> _bgmList = new List<BGMData>();

        public List<BGMData> BGMs { get { return _bgmList; } }
    }
}