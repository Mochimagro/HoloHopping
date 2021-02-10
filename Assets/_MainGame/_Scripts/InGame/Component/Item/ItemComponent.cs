using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

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

        public IObservable<Unit> OnStopParticle;

        [SerializeField] private List<ParticleSystem> _tailPaticles = new List<ParticleSystem>();

        [SerializeField] private SpriteRenderer _spriteRenderer = null;
        [SerializeField] private Collider _collider = null;

        public void Init(Entity.ItemEntity entity)
        {
            _entity = entity;
        }

        public bool TailParticlesLoop
        {
            set
            {
                foreach (var item in _tailPaticles)
                {
                    var main = item.main;
                    main.loop = value;
                }
            }

        }

        public void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(TagName.CHARACTER))
            {
                KillObject();
                _entity.GetPos = transform.position;
                _entity.GetText = _entity.ItemMode == Data.ItemMode.Score ? "+" + _entity.Score : _entity.ItemMode.ToString();
                _entity.SEScene = _entity.ItemMode == Data.ItemMode.Score ? Enum.SEScene.ScoreItem : Enum.SEScene.SpecialItem;
                _onGetItem.OnNext(_entity);

            }
        }

        public void KillObject()
        {

            _spriteRenderer.color = Color.clear;
            _collider.enabled = false;
            TailParticlesLoop = false;

            if (_tailPaticles.Count > 0)
            {

                OnStopParticle = _tailPaticles[0].OnDisableAsObservable();

                OnStopParticle.Subscribe(_ =>
                {
                    GameObject.Destroy(this.gameObject);
                });
            }
            else
            {
                GameObject.Destroy(this.gameObject);
            }
        }

    }
}