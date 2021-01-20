using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Data
{
    [CreateAssetMenu(menuName =
        MenuName.Format.DATA +
        MenuName.Attribute.ITEM +
        MenuName.Type.LIST,
        fileName = "ItemList")]
    public class ItemListData : ScriptableObject
    {
        [SerializeField] private List<ItemData> _items = new List<ItemData>();
    }
}