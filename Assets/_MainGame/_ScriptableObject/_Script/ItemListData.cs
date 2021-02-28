using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Entity
{
    public class HighScoreItem
    {
        public HighScoreItem(Data.HighScoreItem data)
        {
            Entity = new ItemEntity(data.Data);
            BorderStageItemCount = data.BorderStageCount;
        }

        public ItemEntity Entity { get; set; }
        public int BorderStageItemCount { get; set; }
    }

    public class ItemListEntity
    {
        public ItemListEntity(Data.ItemListData data)
        {
            NormalScoreItem = new ItemEntity(data.NormalItemData);

            HighScoreItems = new List<HighScoreItem>();

            foreach (var item in data.HighScoreItems)
            {
                HighScoreItems.Add(new HighScoreItem(item));
            }

            SpecialItems = new List<ItemEntity>();

            foreach (var item in data.SpecialItems)
            {
                SpecialItems.Add(new ItemEntity(item));
            }

            FeverItem = new ItemEntity(data.FeverScoreItem);

        }
        public ItemEntity NormalScoreItem { get; private set; }
        public List<HighScoreItem> HighScoreItems { get; private set; }
        public List<ItemEntity> SpecialItems { get; private set; }
        public ItemEntity FeverItem { get; private set; }


    }
}


namespace HoloHopping.Data
{
    [System.Serializable]
    public class HighScoreItem
    {
        [SerializeField] private ItemData _data = null;
        [SerializeField] private int _borderStageItemCount = 999;

        public ItemData Data { get => _data; set => _data = value; }
        public int BorderStageCount { get => _borderStageItemCount; set => _borderStageItemCount = value; }
    }

    [CreateAssetMenu(menuName =
        MenuName.Format.DATA +
        MenuName.Attribute.ITEM +
        MenuName.Type.LIST,
        fileName = "ItemList")]
    public class ItemListData : ScriptableObject
    {
        [SerializeField] private ItemData _normalScoreItem = null;

        [SerializeField] private List<HighScoreItem> _highScoreItems = new List<HighScoreItem>();

        [SerializeField] private List<ItemData> _specialItems = new List<ItemData>();
        [SerializeField] private ItemData _feverScoreItem = null;

        public ItemData NormalItemData { get => _normalScoreItem; }
        public List<HighScoreItem> HighScoreItems { get => _highScoreItems; }
        public List<ItemData> SpecialItems { get => _specialItems; }
        public ItemData FeverScoreItem { get => _feverScoreItem; }
    }
}