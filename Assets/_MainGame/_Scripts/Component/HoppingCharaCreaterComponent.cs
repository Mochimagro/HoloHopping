using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Arbor;

namespace HoloHopping.Component
{

    public class HoppingCharaCreaterComponent : MonoBehaviour
    {
        [SerializeField] private ParameterContainer _systemParameter = null;
        [SerializeField] private Data.HoppingCharacterList _hoppingCharacterList = null;

        public HoppingCharacterComponent CreateHoppingCharacter()
        {
            var chara = Instantiate(_hoppingCharacterList.RandomData.CharaComponent);

            return chara;

        }

    }
}