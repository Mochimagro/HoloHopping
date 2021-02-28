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
        private ReactiveCollection<Component.ItemComponent> _fieldScoreItems = null;

        public IObservable<int> OnClearFieldItems => _fieldScoreItems.ObserveCountChanged().Where(count => count <= 0);

        public ItemCreaterModel(Entity.ItemListEntity itemListEntity)
        {
            _itemListEntity = itemListEntity;
            _fieldScoreItems = new ReactiveCollection<Component.ItemComponent>();
            _highScoreItem = _itemListEntity.HighScoreItems.OrderBy(e => e.BorderStageItemCount).ToList();

            _fieldScoreItems.ObserveCountChanged().Subscribe(value =>
            {
                Debug.Log("FieldItemCount : " + value);
            });


        }

        public Entity.ItemEntity NormalScoreItem { get => _itemListEntity.NormalScoreItem; }

        public List<GameObject> ScoreItemObjects { get => _fieldScoreItems.Select(itemComponent => itemComponent.gameObject).ToList(); }

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

        public void AddFieldItem(Component.ItemComponent item)
        {
            _fieldScoreItems.Add(item);

            item.OnDeathItem.Subscribe(e =>
            {
                RemoveFieldItem(e.TargetComponent);
            });

        }

        public void RemoveFieldItem(Component.ItemComponent item)
        {
            _fieldScoreItems.Remove(item);
        }


    }
}