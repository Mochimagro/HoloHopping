using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Enum
{
    public struct AnimationParameterMainPlayer
    {
        public const string MOVE = "move";
        public const string CHARGE = "Charging";
    }
}

namespace HoloHopping.Component
{

    public class MainPlayerComponent : MonoBehaviour
    {
        [SerializeField] private Arbor.ArborFSM _arborFSM = null;
        [SerializeField] private Arbor.ParameterContainer _parameter = null;

        [SerializeField] private Data.CharacterList _DebugCharacters = null;

        [SerializeField] private Arbor.GlobalParameterContainer _globalParameterContainer = null;

        private Animator _animator = null;

        public void DebugInit()
        {
            var selectName = _globalParameterContainer.container.GetString("SelectCharacter");
            Init(new Entity.CharacterEntityList(_DebugCharacters).EntitiesDictionary[selectName]);
        }

        public void Init(Entity.CharacterEntity entity)
        {
            var com = Instantiate(entity.CharacterComponent, this.transform);
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