using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Arbor;

namespace HoloHopping.Entity
{

    public class HoppingActionEntity
    {
       public HoppingActionEntity(Vector3 pos, Enum.FXType type)
        {
            Position = pos;
            HoppingType = type;
        }

        public Vector3 Position { get; private set; }
        public Enum.FXType HoppingType { get; private set; }

    }
}

namespace HoloHopping.Component
{
    using Entity;
    public class HoppingCharacterComponent : MonoBehaviour
    {
        [SerializeField] private ParameterContainer _parameter = null;
        private Rigidbody _rigidbody => GetComponent<Rigidbody>();

        public IObservable<HoppingCharacterComponent> OnMiss => _onMiss;
        private Subject<HoppingCharacterComponent> _onMiss = new Subject<HoppingCharacterComponent>();

        public IObservable<HoppingActionEntity> OnHop  =>_onHop;
        private Subject<HoppingActionEntity> _onHop = new Subject<HoppingActionEntity>();

        public Entity.HoppingCharacter SetEntity
        {
            set
            {
                _parameter.SetComponent("StartWay", value.StartWay);
                _parameter.SetVector3("StartRotation", value.StartRotation);
            }
        }

        public void Init()
        {

        }

        public bool UseGravity
        {
            set { _rigidbody.useGravity = value; }
        }

        public void InvokeMiss()
        {
            _onMiss.OnNext(this);
        }

        public void InvokeHop(Enum.FXType type)
        {
            _onHop.OnNext(new HoppingActionEntity(this.transform.position, type));
        }

    }
}