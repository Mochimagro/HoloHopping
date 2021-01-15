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
    }
}