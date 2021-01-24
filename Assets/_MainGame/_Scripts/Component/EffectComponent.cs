using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Component
{

    public class EffectComponent : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _particleSystems;

        public Color FXColor
        {
            set
            {
                foreach (var fx in _particleSystems)
                {
                    var module = fx.main;
                    module.startColor = value;
                }
            }
        }

    }
}