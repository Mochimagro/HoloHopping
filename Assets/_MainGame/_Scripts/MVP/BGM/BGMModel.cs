using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Model
{

    public class BGMModel
    {
        public BGMModel(Entity.BGMEntityList list)
        {
            _bgmDictionary = new Dictionary<Enum.BGMScene, Entity.BGMEntity>();

            foreach (var e in list.Entities)
            {
                _bgmDictionary.Add(e.BGMScene, e);
            }

        }

        private Dictionary<Enum.BGMScene, Entity.BGMEntity> _bgmDictionary = null;

        public AudioClip GetSceneBGM(Enum.BGMScene scene)
        {
            return _bgmDictionary[scene].AudioFile;
        }

    }
}