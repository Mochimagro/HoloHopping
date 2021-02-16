using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Model
{
    public class RankingModel
    {
        private ScoreModel _scoreModel = null;

        public RankingModel(ScoreModel scoreModel)
        {
            _scoreModel = scoreModel;

        }

        public int ResultScore
        {
            get => _scoreModel.GetScore;
        }


    }
}