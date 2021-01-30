using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using Arbor;

namespace HoloHopping.Entity
{
    public class HoppingCharacterEntity
    {
        public HoppingCharacterEntity(Data.HoppingCharacterData data, Vector3 charaRot, Waypoint startWay, Vector3 jumpVec)
        : this(data.CharaComponent, charaRot, startWay, jumpVec)
        {
        }

        public HoppingCharacterEntity(Component.HoppingCharacterComponent com, Vector3 charaRot, Waypoint startWay, Vector3 jumpVec)
        {

            Component = com;
            CharaRotation = charaRot;
            StartWay = startWay;
            JumpVector = jumpVec;
        }


        public Component.HoppingCharacterComponent Component { get; private set; }
        public Vector3 CharaRotation { get; private set; }
        public Waypoint StartWay { get; private set; }
        public Vector3 JumpVector { get; private set; }
    }
}

namespace HoloHopping.Component
{

    public class HoppingCharaCreaterComponent : MonoBehaviour
    {
        [SerializeField] private ParameterContainer _systemParameter = null;
        [SerializeField] private HoppingCharacterComponent _hoppingCharacterBase = null;

        [SerializeField] private Data.CharacterList _DebugCharacters = null;

        public IObservable<Entity.MissInfoEntity> OnCharacterMiss => _onCharacterMiss;
        private Subject<Entity.MissInfoEntity> _onCharacterMiss = new Subject<Entity.MissInfoEntity>();

        public IObservable<Entity.FXCreateEntity> OnHopCharacter => _onHopCharacter;
        private Subject<Entity.FXCreateEntity> _onHopCharacter = new Subject<Entity.FXCreateEntity>();

        public IObservable<HoppingCharacterComponent> OnCreateCharacter => _onCreateCharacter;
        private Subject<HoppingCharacterComponent> _onCreateCharacter = new Subject<HoppingCharacterComponent>();

        /*
        public HoppingCharacterComponent CreateHoppingCharacter()
        {
            var entity = new Entity.HoppingCharacterEntity(_hoppingCharacterList.RandomData, this.CharacterRotation, this.StartWay, this.JumpVector);


            var chara = Instantiate(entity.Component, CreatePosition, Quaternion.identity);

            chara.OnMiss.TakeUntilDestroy(chara).Subscribe(value =>
            {
                _onCharacterMiss.OnNext(value);
            });

            chara.OnHop.TakeUntilDestroy(chara).Subscribe(value =>
            {
                _onHopCharacter.OnNext(value);
            });

            chara.Init(entity);

            _onCreateCharacter.OnNext(chara);
            return chara;

        }
        */

        public HoppingCharacterComponent CreateHoppingCharacter()
        {
            var hop = Instantiate(_hoppingCharacterBase, CreatePosition, Quaternion.identity, this.transform);

            var chara = Instantiate(new Entity.CharacterEntityList(_DebugCharacters).GetRondomCharacter.CharacterComponent, hop.transform);

            var hopEntity = new Entity.HoppingCharacterEntity(hop, this.CharacterRotation, this.StartWay, this.JumpVector);

            hop.SetCharacterComponent = chara;

            hop.OnMiss.TakeUntilDestroy(chara).Subscribe(value =>
            {
                _onCharacterMiss.OnNext(value);
            });

            hop.OnHop.TakeUntilDestroy(chara).Subscribe(value =>
            {
                _onHopCharacter.OnNext(value);
            });

            hop.Init(hopEntity);

            _onCreateCharacter.OnNext(hop);

            return hop;
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

        public Vector3 JumpVector
        {
            get
            {
                return PlayerIsLeft ? new Vector3(-2, 10, 0) : new Vector3(2, 10, 0);
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