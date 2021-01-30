using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Arbor;

namespace HoloHopping.Enum
{
    public struct AnimationParameterHopPlayer
    {
        public const string JUMP = "Jump";
        public const string BEND = "Bend";
        public const string MISS = "Miss";
        public const string END_UP = "endUp";
        public const string START_DOWN = "startDown";

    }
}

namespace HoloHopping.Entity
{

    public class FXCreateEntity
    {
        public FXCreateEntity()
        {

        }
        public FXCreateEntity(Vector3 pos, Enum.FXType type)
        {
            SetData(pos, type);
        }

        public FXCreateEntity(Vector3 pos, Enum.FXType type, Color fxColor)
        {
            SetData(pos, type);
            FXColor = fxColor;
        }


        public FXCreateEntity SetData(Vector3 pos, Enum.FXType type)
        {
            Position = pos;
            FXType = type;

            return this;
        }

        public Vector3 Position { get; private set; }
        public Enum.FXType FXType { get; private set; }
        public Color FXColor { get; private set; }

    }

    public class MissInfoEntity
    {
        public MissInfoEntity(FXCreateEntity entity, Component.HoppingCharacterComponent component)
        {
            FXCreateEntity = entity;
            HoppingCharacterComponent = component;
        }

        public FXCreateEntity FXCreateEntity { get; private set; }
        public Component.HoppingCharacterComponent HoppingCharacterComponent { get; private set; }

    }
}

namespace HoloHopping.Component
{
    using Entity;
    public class HoppingCharacterComponent : MonoBehaviour
    {
        [SerializeField] private ParameterContainer _parameter = null;

        private CharacterComponent _characterComponent = null;
        public CharacterComponent SetCharacterComponent { set { _characterComponent = value; } }

        private FXCreateEntity _actionEntity = null;

        private Rigidbody _rigidbody => GetComponent<Rigidbody>();

        public IObservable<MissInfoEntity> OnMiss => _onMiss;
        private Subject<MissInfoEntity> _onMiss = new Subject<MissInfoEntity>();

        public IObservable<FXCreateEntity> OnHop => _onHop;
        private Subject<FXCreateEntity> _onHop = new Subject<FXCreateEntity>();

        public HoppingCharacterEntity SetEntity
        {
            set
            {
                _parameter.SetComponent("StartWay", value.StartWay);
                _parameter.SetVector3("StartRotation", value.CharaRotation);
                _parameter.SetVector3("StartJumpVector", value.JumpVector);
                _parameter.SetComponent<Animator>("CharaAnimator", _characterComponent.Animator);
            }
        }

        public void Init(HoppingCharacterEntity entity)
        {
            this.gameObject.SetActive(true);
            SetEntity = entity;
            _characterComponent.HoppingObjectSetActive = false;
            _characterComponent.Animator.SetLayerWeight(1, 1);
            _actionEntity = new FXCreateEntity();
        }

        public bool UseGravity
        {
            set { _rigidbody.useGravity = value; }
        }

        public void InvokeMiss()
        {
            _onMiss.OnNext(new MissInfoEntity
                (
                    new FXCreateEntity(this.transform.position, Enum.FXType.Miss),
                    this
                )
                );
        }

        public void InvokeHop(Enum.FXType type)
        {
            _onHop.OnNext(_actionEntity.SetData(this.transform.position, type));
        }

        public void SetBoolAnimation(string parameterName, bool value)
        {
            _characterComponent.Animator.SetBool(parameterName, value);
        }

        public void SetTriggerAnimation(string parameterName)
        {
            _characterComponent.Animator.SetTrigger(parameterName);
        }

    }
}