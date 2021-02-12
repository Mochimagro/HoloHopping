using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Enum
{
    public enum BGMScene
    {
        None,
        Ready,
        Start,
        MainGame,
        GameOver,
        Fever,
        FeverPrefect
    }
}

namespace HoloHopping.Entity
{
    public class BGMEntity
    {
        public BGMEntity(Data.BGMData data)
        {
            AudioFile = data.AudioFile;
            BGMScene = data.BGMScene;
            BGMSceneName = data.BGMSceneName;
        }

        public AudioClip AudioFile { get; private set; }
        public string BGMSceneName { get; private set; }
        public Enum.BGMScene BGMScene { get; private set; }
    }
}

namespace HoloHopping.Data
{
    [CreateAssetMenu(menuName =
        MenuName.Format.DATA +
        MenuName.Attribute.BGM +
        MenuName.Type.PARAMETER
        , fileName = "NewBGMData")]
    public class BGMData : ScriptableObject
    {
        [SerializeField] private AudioClip _audioFile = null;
        [SerializeField] private string _bgmSceneName = null;
        [SerializeField] private Enum.BGMScene _bgmScene = default;


        public AudioClip AudioFile { get { return _audioFile; } }
        public string BGMSceneName { get { return _bgmSceneName; } }
        public Enum.BGMScene BGMScene { get { return _bgmScene; } }

    }
}