using System.Collections;
using UnityEngine;

namespace HoloHopping.Component
{
    public class CharacterComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator = null;
        [SerializeField] private GameObject _hopping = null;

        public void Init()
        {

        }

        public bool HoppingObjectSetActive { set { _hopping.SetActive(value); } }
        public Animator Animator { get { return _animator; } }
        public SkinnedMeshRenderer HoppingSkin { get { return _hopping.GetComponent<SkinnedMeshRenderer>(); } }
    }
}