using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Model
{
    public class FeverModel
    {
        public FeverModel()
        {
            _feverScoreItems = new List<Component.ItemComponent>();
            _itemCount = new IntReactiveProperty(0);
        }

        private List<Component.ItemComponent> _feverScoreItems = null;

        private IntReactiveProperty _itemCount = null;
        public IObservable<int> OnItemCount => _itemCount;

        public void AddItem(Component.ItemComponent item)
        {
            _feverScoreItems.Add(item);
            _itemCount.Value = _feverScoreItems.Count;
        }

        public void RemoveItem(Component.ItemComponent item)
        {
            _feverScoreItems.Remove(item);
            _itemCount.Value = _feverScoreItems.Count;
        }

        public void AllRemoveItem()
        {
            foreach (var item in _feverScoreItems)
            {
                GameObject.Destroy(item.gameObject);
            }

            _feverScoreItems.Clear();
        }

    }
}