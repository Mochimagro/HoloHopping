using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Model
{

    public class SEModel
    {
        public SEModel(Entity.SEEntityList entityList)
        {
            _seClipDictionary = new Dictionary<Enum.SEScene, AudioClip>();

            foreach (var entity in entityList.List)
            {
                _seClipDictionary.Add(entity.SEScene, entity.AudioClip);
            }

        }

        private Dictionary<Enum.SEScene, AudioClip> _seClipDictionary = null;

        public AudioClip GetSEClip(Enum.SEScene scene)
        {

            return _seClipDictionary[scene];
        }

    }
}