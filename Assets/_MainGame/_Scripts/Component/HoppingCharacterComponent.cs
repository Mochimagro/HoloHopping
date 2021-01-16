using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Arbor;

namespace HoloHopping.Component
{

    public class HoppingCharacterComponent : MonoBehaviour
    {
        [SerializeField] private ParameterContainer _parameter = null;
        private Rigidbody _rigidbody => GetComponent<Rigidbody>();

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

    }
}