using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Entity
{

    public interface IEntity
    {

    }
    public class ItemEntity:IEntity
    {
        public ItemEntity(Data.ItemData data)
        {
            Component = data.PrefabComponent;
            Score = data.Score;
        }

        public Component.ItemComponent Component { get; private set; }
        public int Score { get; private set; }

    }
}

namespace HoloHopping.Data
{
    [CreateAssetMenu(menuName =
        MenuName.Format.DATA +
        MenuName.Attribute.ITEM +
        MenuName.Type.PARAMETER,
        fileName = "NewItemName")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private Component.ItemComponent _prefab = null;
        [SerializeField] private int _score = 100;

        public Component.ItemComponent PrefabComponent { get { return _prefab; } }
        public int Score { get { return _score; } }
    }
}