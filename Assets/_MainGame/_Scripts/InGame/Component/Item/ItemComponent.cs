using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace HoloHopping.Entity
{
    public class ItemGetEntity
    {
        public ItemGetEntity
            (
                ItemEntity itemEntity,
                Component.ItemComponent thisItemComponent,
                string getText,
                Vector3 getPosition,
                Enum.SEScene seScene
            )
        {
            TargetComponent = thisItemComponent;
            Score = itemEntity.Score;
            ItemMode = itemEntity.ItemMode;
            ItemColor = itemEntity.ItemColor;

            GetText = getText;
            GetPosition = getPosition;
            SEScene = seScene;
        }

        public Component.ItemComponent TargetComponent { get; private set; }
        public int Score { get; private set; }
        public string GetText { get; private set; }
        public Data.ItemMode ItemMode { get; private set; }
        public Color ItemColor { get; private set; }
        public Vector3 GetPosition { get; private set; }
        public Enum.SEScene SEScene { get; private set; }
        public FXCreateEntity FXCreateEntity { get; set; }

    }
}

namespace HoloHopping.Component
{
    using Entity;
    public interface IItem
    {
        IObservable<ItemGetEntity> OnGetItem { get; }
        //void Init(Entity.IEntity entity);
    }

    public class ItemComponent : MonoBehaviour, IItem
    {
        private ItemEntity _entity = null;
        private ItemGetEntity _itemGetEntity = null;

        /// <summary>
        /// 入手したときのイベント
        /// </summary>
        public IObservable<ItemGetEntity> OnGetItem => _onGetItem.TakeUntilDestroy(this.gameObject);
        private Subject<ItemGetEntity> _onGetItem = new Subject<ItemGetEntity>();

        /// <summary>
        /// 経過時間で消去されるときのイベント
        /// </summary>
        public IObservable<ItemGetEntity> OnDeathItem => _onDeathItem;
        private Subject<ItemGetEntity> _onDeathItem = new Subject<ItemGetEntity>();

        /// <summary>
        /// TailParticleが終了したときのイベント
        /// </summary>
        public IObservable<Unit> OnStopParticle;

        [SerializeField] private List<ParticleSystem> _tailPaticles = new List<ParticleSystem>();

        [SerializeField] private SpriteRenderer _spriteRenderer = null;
        [SerializeField] private Collider _collider = null;

        public void Init(Entity.ItemEntity entity)
        {
            _entity = entity;
        }


        public void AllStopTailParticle()
        {
            foreach (var item in _tailPaticles)
            {
                item.Stop();
            }

        }

        public void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(TagName.CHARACTER))
            {
                KillObject();

                _itemGetEntity = new ItemGetEntity
                    (
                        _entity,
                        this,
                        _entity.ItemMode == Data.ItemMode.Score ? "+" + _entity.Score : _entity.ItemMode.ToString(),
                        transform.position,
                        _entity.ItemMode == Data.ItemMode.Score ? Enum.SEScene.ScoreItem : Enum.SEScene.SpecialItem
                    );

                _onGetItem.OnNext(_itemGetEntity);

            }
        }

        public void KillObject()
        {
            _itemGetEntity = new ItemGetEntity(_entity, this, string.Empty, Vector3.zero, default);

            _spriteRenderer.color = Color.clear;
            _collider.enabled = false;
            AllStopTailParticle();

            _onDeathItem.OnNext(_itemGetEntity);
            _onDeathItem.OnCompleted();

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