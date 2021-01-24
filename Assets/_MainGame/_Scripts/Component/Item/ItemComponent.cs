using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Component
{
    using Entity;
    public interface IItem
    {
        IObservable<ItemEntity> OnGetItem { get; }
        //void Init(Entity.IEntity entity);
    }

    public class ItemComponent : MonoBehaviour, IItem
    {
        private ItemEntity _entity = null;

        public IObservable<ItemEntity> OnGetItem => _onGetItem.TakeUntilDestroy(this.gameObject);
        private Subject<ItemEntity> _onGetItem = new Subject<ItemEntity>();

        public void Init(Entity.ItemEntity entity)
        {
            _entity = entity;
        }

        public void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(TagName.CHARACTER))
            {
                _entity.GetPos = transform.position;
                _entity.GetText = _entity.ItemMode == Data.ItemMode.Score ? "+" + _entity.Score : _entity.ItemMode.ToString();
                _entity.SEScene = _entity.ItemMode == Data.ItemMode.Score ? Enum.SEScene.ScoreItem : Enum.SEScene.SpecialItem;
                _onGetItem.OnNext(_entity);
                Destroy(this.gameObject);
            }
        }

    }
}