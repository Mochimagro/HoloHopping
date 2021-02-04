using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Component
{
    using Data;
    public interface ISpechalItemEffectComponent
    {
        void EffectInvoke();
    }

    [System.Serializable]
    public class SpecialEffect
    {
        [SerializeField] private ItemMode _mode = default;
        [SerializeField] private Arbor.ArborFSM _effectFSM = null;

        public ItemMode Mode { get => _mode; }
        public Arbor.ArborFSM FSM { get => _effectFSM; }

    }

    public class SpecialItemEffectComponent : MonoBehaviour
    {
        [SerializeField] private List<SpecialEffect> _specialEffects = new List<SpecialEffect>();

        private Dictionary<ItemMode, Arbor.ArborFSM> _effectDictionary = null;

        public void Init()
        {
            _effectDictionary = new Dictionary<ItemMode, Arbor.ArborFSM>();


            foreach (var item in _specialEffects)
            {
                _effectDictionary.Add(item.Mode, item.FSM);
            }

        }

        public void InvokeEffect(ItemMode mode)
        {
            if (mode == ItemMode.Score || mode == ItemMode.None) return;

            var fsm = Instantiate(_effectDictionary[mode], this.transform);

            fsm.gameObject.SetActive(true);

            fsm.Play();


        }

    }
}