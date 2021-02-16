using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Presenter
{
    using Model;
    using View;
    public class RankingPresenter : MonoBehaviour
    {
        [SerializeField] private RankingView _rankingView = null;
        private RankingModel _rankingModel = null;

        public void Init(ScoreModel scoreModel)
        {
            _rankingModel = new RankingModel(scoreModel);
            _rankingView.Init();


            Bind();
        }

        private void Bind()
        {


        }

        public void ShowRankingBoard()
        {
            _rankingView.ShowRankingView(_rankingModel.ResultScore);
        }

    }
}