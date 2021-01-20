using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using Arbor;


namespace HoloHopping.Entity
{
    public class HoppingCharacter
    {
        public HoppingCharacter(Data.HoppingCharacterData data)
        {
            Component = data.CharaComponent;
        }

        public Component.HoppingCharacterComponent Component { get; private set; }
        public Vector3 StartRotation { get; set; }
        public Waypoint StartWay { get; set; }
    }
}

namespace HoloHopping.Component
{

    public class HoppingCharaCreaterComponent : MonoBehaviour
    {
        [SerializeField] private ParameterContainer _systemParameter = null;
        [SerializeField] private Data.HoppingCharacterList _hoppingCharacterList = null;

        public IObservable<HoppingCharacterComponent> OnCharacterMiss => _onCharacterMiss;
        private Subject<HoppingCharacterComponent> _onCharacterMiss = new Subject<HoppingCharacterComponent>();

        public IObservable<HoppingCharacterComponent> OnCreateCharacter => _onCreateCharacter;
        private Subject<HoppingCharacterComponent> _onCreateCharacter = new Subject<HoppingCharacterComponent>();

        public HoppingCharacterComponent CreateHoppingCharacter()
        {
            var entity = new Entity.HoppingCharacter(_hoppingCharacterList.RandomData);
            entity.StartWay = this.StartWay;
            entity.StartRotation = this.CharacterRotation;

            var chara = Instantiate(entity.Component,CreatePosition,Quaternion.identity);

            chara.OnMiss.TakeUntilDisable(chara).Subscribe(value =>
            {
                _onCharacterMiss.OnNext(value);
            });

            chara.SetEntity = entity;

            _onCreateCharacter.OnNext(chara);
            return chara;

        }

        public Vector3 CreatePosition
        {
            get
            {
                return StartWay.GetPoint(0).position;
            }
        }

        public Vector3 CharacterRotation
        {
            get
            {
                return PlayerIsLeft ? new Vector3(0, 240, 0) : new Vector3(0, 120, 0);
            }
        }

        public Waypoint StartWay
        {
            get
            {
                return PlayerIsLeft ? _systemParameter.GetComponent<Waypoint>("WayRight") : _systemParameter.GetComponent<Waypoint>("WayLeft");
            }
        }

        private bool PlayerIsLeft
        {
            get 
            {
                var playerPos = _systemParameter.GetTransform("Player");
                return playerPos.position.x <= 0;
            }
        }
    }
}