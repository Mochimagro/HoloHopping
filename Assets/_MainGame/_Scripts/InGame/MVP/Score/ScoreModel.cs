using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Model
{

    public class ScoreModel
    {
        private IntReactiveProperty _score = new IntReactiveProperty();

        public IObservable<int> OnValueChangedScore => _score;

        public int GetScore
        {
            get { return _score.Value; }
        }

        public int AddScore
        {
            set
            {
                _score.Value += value;
                //Debug.Log(_score.Value);
            }
        }

        public ScoreModel()
        {
            _score.Value = 0;
        }

    }
}