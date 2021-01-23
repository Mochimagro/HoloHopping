using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Entity
{
    public class FXEntityList
    {
        public FXEntityList(Data.FXList list)
        {
            Entities = new List<FXEntity>();

            foreach(var data in list.List)
            {
                Entities.Add(new FXEntity(data));
            }

        }

        public List<FXEntity> Entities { get; private set; }

    }
}

namespace HoloHopping.Data
{
    [CreateAssetMenu(menuName = 
        MenuName.Format.DATA + 
        MenuName.Attribute.FX + 
        MenuName.Type.LIST,
        fileName = "FXList")]
    public class FXList : ScriptableObject
    {
        [SerializeField] private List<FXData> _list = null;

        public List<FXData> List { get { return _list; } }
    }
}