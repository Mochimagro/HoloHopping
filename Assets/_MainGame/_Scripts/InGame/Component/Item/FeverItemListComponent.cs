using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Component
{

    public class FeverItemListComponent : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _positions = new List<GameObject>();

        public List<GameObject> Positions { get => _positions; }

    }
}