using System;
using UniRx;
using UnityEngine;
using HoloHopping.Model;
using HoloHopping.View;

namespace HoloHopping.Presenter
{

    public class ScorePresenter : MonoBehaviour
    {
        [SerializeField] private ScoreView _scoreView = null;
        private ScoreModel _scoreModel = null;

        public int Score
        {
            get { return _scoreModel.GetScore; }
        }

        public void Init(out ScoreModel model)
        {
            _scoreModel = new ScoreModel();
            model = _scoreModel;
            _scoreView.Init();
            Bind();
        }

        private void Bind()
        {
            _scoreModel.OnValueChangedScore.Subscribe(value =>
            {
                _scoreView.SetScoreText = value;
            });
        }

        public ScoreModel GetModel
        {
            get => _scoreModel;
        }

    }
}