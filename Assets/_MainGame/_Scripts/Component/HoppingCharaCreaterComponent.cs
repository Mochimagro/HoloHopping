using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Arbor;


namespace HoloHopping.Entity
{
    public class HoppingCharacter
    {
        public HoppingCharacter(Data.HoppingCharacterData data)
        {

        }


    }
}

namespace HoloHopping.Component
{

    public class HoppingCharaCreaterComponent : MonoBehaviour
    {
        [SerializeField] private ParameterContainer _systemParameter = null;
        [SerializeField] private Data.HoppingCharacterList _hoppingCharacterList = null;

        public HoppingCharacterComponent CreateHoppingCharacter()
        {
            var chara = Instantiate(_hoppingCharacterList.RandomData.CharaComponent,CreatePosition,Quaternion.identity);

            return chara;

        }

        public Vector3 CreatePosition
        {
            get
            {
                var playerPos = _systemParameter.GetTransform("Player");

                return playerPos.position.x >= 0 ? _systemParameter.GetComponent<Waypoint>("WayLeft").GetPoint(0).position : _systemParameter.GetComponent<Waypoint>("WayRight").GetPoint(0).position;

            }
        }


    }
}