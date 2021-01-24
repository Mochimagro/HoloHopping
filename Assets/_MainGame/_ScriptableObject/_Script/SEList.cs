using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Entity
{
    public class SEEntityList
    {
        public SEEntityList(Data.SEList list)
        {
            List = new List<SEEntity>();

            foreach (var data in list.Datas)
            {
                List.Add(new SEEntity(data));
            }

        }

        public List<SEEntity> List { get; private set; }
    }
}

namespace HoloHopping.Data
{
    [CreateAssetMenu(menuName =
        MenuName.Format.DATA +
        MenuName.Attribute.SE +
        MenuName.Type.LIST,
        fileName = "SEList")]
    public class SEList : ScriptableObject
    {
        [SerializeField] private List<SEData> _datas = new List<SEData>();

        public List<SEData> Datas { get { return _datas; } }
    }
}