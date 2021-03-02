using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

namespace HoloHopping.Model
{
    public class ItemCreaterModel
    {
        private Entity.ItemListEntity _itemListEntity = null;
        private List<Entity.HighScoreItem> _highScoreItem = null;
        private List<Entity.ItemEntity> _specialItems = null;
        private ReactiveCollection<Component.ItemComponent> _fieldScoreItems = null;
        private List<Component.ItemComponent> _fieldSpecialItems = null;

        public IObservable<int> OnClearFieldItems => _fieldScoreItems.ObserveCountChanged().Where(count => count <= 0);

        public ItemCreaterModel(Entity.ItemListEntity itemListEntity)
        {
            _itemListEntity = itemListEntity;
            _fieldScoreItems = new ReactiveCollection<Component.ItemComponent>();
            _fieldSpecialItems = new List<Component.ItemComponent>();
            _highScoreItem = _itemListEntity.HighScoreItems.OrderBy(e => e.BorderStageItemCount).ToList();
            _specialItems = _itemListEntity.SpecialItems.ToList();

            _fieldScoreItems.ObserveCountChanged().Subscribe(value =>
            {
                Debug.Log("FieldItemCount : " + value);
            });


        }

        public Entity.ItemEntity NormalScoreItem { get => _itemListEntity.NormalScoreItem; }

        public List<GameObject> ScoreItemObjects { get => _fieldScoreItems.Select(itemComponent => itemComponent.gameObject).ToList(); }

        public List<GameObject> SpecialItemObjects { get => _fieldSpecialItems.Select(itemComponent => itemComponent.gameObject).ToList(); }

        public Entity.ItemEntity GetScoreItem
        {
            get
            {
                foreach (var target in _highScoreItem)
                {
                    if (_fieldScoreItems.Count < target.BorderStageItemCount)
                    {
                        return target.Entity;
                    }
                }
                return NormalScoreItem;
            }
        }

        public Entity.ItemEntity GetSpecialItem
        {
            get
            {
                return _specialItems[UnityEngine.Random.Range(0, _specialItems.Count)];
            }
        }

        public Entity.ItemEntity GetStarRushItem
        {
            get => _itemListEntity.StarRushItem;
        }

        public Entity.ItemEntity GetFeverScoreItem
        {
            get => _itemListEntity.FeverItem;
        }

        public void AddFieldItem(Component.ItemComponent item)
        {
            _fieldScoreItems.Add(item);

            item.OnDeathItem.Subscribe(e =>
            {
                RemoveFieldScoreItem(e.TargetComponent);
            });

        }

        public void AddFieldSpecialItem(Component.ItemComponent item)
        {
            _fieldSpecialItems.Add(item);

            item.OnDeathItem.Subscribe(e =>
            {
                RemoveFieldSpecialItem(e.TargetComponent);
            });

        }

        public void RemoveFieldScoreItem(Component.ItemComponent item)
        {
            _fieldScoreItems.Remove(item);
        }

        public void RemoveFieldSpecialItem(Component.ItemComponent item)
        {
            _fieldSpecialItems.Remove(item);
        }

    }
}