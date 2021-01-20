using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Component
{

    public interface IItem
    {
        IObservable<int> OnGetItem { get; }
        //void Init(Entity.IEntity entity);
    }

    public class ItemComponent : MonoBehaviour,IItem
    {
        private int _score = -1;

        public IObservable<int> OnGetItem => _onGetItem.TakeUntilDestroy(this.gameObject);
        private Subject<int> _onGetItem = new Subject<int>();

        public void Init(Entity.ItemEntity entity)
        {
            _score = entity.Score;
        }

        public void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(TagName.CHARACTER))
            {
                Destroy(this.gameObject);
                _onGetItem.OnNext(_score);
            }
        }

    }
}