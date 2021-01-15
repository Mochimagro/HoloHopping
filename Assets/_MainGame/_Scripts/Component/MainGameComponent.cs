using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Component
{

    public class MainGameComponent : MonoBehaviour
    {
        [SerializeField] private HoppingCharaCreaterComponent _hoppingCharaCreaterComponent = null;

        private void Start()
        {
            _hoppingCharaCreaterComponent.CreateHoppingCharacter();
        }

    }
}