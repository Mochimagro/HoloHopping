using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEditor.Animations;

namespace HoloHopping.Enum
{
    public class AnimationParameterName
    {
        public const string MOVE = "move";
        public const string CHARGE = "Charge";
    }
}

namespace HoloHopping.Component
{

    public class MainPlayerComponent : MonoBehaviour
    {
        [SerializeField] private Arbor.ArborFSM _arborFSM = null;
        [SerializeField] private Arbor.ParameterContainer _parameter = null;

        [SerializeField] private AnimatorController _controller = null;

        [SerializeField] private Data.CharacterList _DebugCharacters = null;

        private Animator _animator = null;

        public void DebugInit()
        {
            Init(new Entity.CharacterEntityList(_DebugCharacters).GetRondomCharacter);
        }

        public void Init(Entity.CharacterEntity entity)
        {
            var com = Instantiate(entity.CharacterComponent, this.transform);
            com.AnimatorController = _controller;
            com.HoppingObjectSetActive = true;
            _parameter.SetComponent("HoppingSkin", com.HoppingSkin);

            _animator = com.Animator;

            _arborFSM.Play();

        }

        public void SetAnimator(string parameterName, bool value)
        {
            _animator.SetBool(parameterName, value);
        }

    }
}