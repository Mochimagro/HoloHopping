using System.Collections.Generic;
using UnityEngine;

namespace HoloHopping.Data
{
    [CreateAssetMenu(menuName =
        MenuName.Format.DATA +
        MenuName.Attribute.HOPPING_CHARACTER +
        MenuName.Type.LIST, fileName ="HoppingCharacterList")]
    public class HoppingCharacterList : ScriptableObject
    {
        [SerializeField] private List<HoppingCharacterData> _hoppingCharacterDatas = new List<HoppingCharacterData>();

        public HoppingCharacterData RandomData
        {
            get { return _hoppingCharacterDatas[Random.Range(0, _hoppingCharacterDatas.Count)]; }
        }

    }
}