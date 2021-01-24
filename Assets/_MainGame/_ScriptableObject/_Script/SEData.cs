using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Enum
{
    public enum SEScene
    {
        None,
        ScoreItem,
        SpecialItem,
    }
}

namespace HoloHopping.Entity
{
    public class SEEntity
    {
        public SEEntity(Data.SEData data)
        {
            AudioClip = data.AudioClip;
            SEScene = data.Scene;
        }

        public AudioClip AudioClip { get; private set; }
        public Enum.SEScene SEScene { get; private set; }
    }
}


namespace HoloHopping.Data
{

    [CreateAssetMenu(menuName =
        MenuName.Format.DATA +
        MenuName.Attribute.SE +
        MenuName.Type.PARAMETER,
        fileName = "NewSE")]
    public class SEData : ScriptableObject
    {
        [SerializeField] private AudioClip _audioClip = null;
        [SerializeField] private Enum.SEScene _scene = default;

        public AudioClip AudioClip
        {
            get { return _audioClip; }
        }

        public Enum.SEScene Scene { get { return _scene; } }

    }
}