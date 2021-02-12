using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Presenter
{
    using Model;
    using System.Collections;
    using View;
    public class BGMPresenter : MonoBehaviour
    {
        [SerializeField] private BGMView _bgmView = null;
        private BGMModel _bgmModel = null;

        [SerializeField] private Data.BGMList _bgmList = null;

        public void Init()
        {
            _bgmModel = new BGMModel(new Entity.BGMEntityList(_bgmList));
            _bgmView.Init();

            Bind();
        }

        private void Bind()
        {

        }

        public void PlayReadySound()
        {
            _bgmView.PlaySimpleSound(_bgmModel.GetSceneBGM(Enum.BGMScene.Ready));
        }

        public void PlayStartSound()
        {
            _bgmView.PlaySound(_bgmModel.GetSceneBGM(Enum.BGMScene.Start));

            _bgmView.OnCompleteSound.Subscribe(_ =>
            {
                _bgmView.PlayLoopBGM(_bgmModel.GetSceneBGM(Enum.BGMScene.MainGame));
            });
        }


        public void ChangeBGM(Enum.BGMScene scene)
        {
            _bgmView.PlayLoopBGM(_bgmModel.GetSceneBGM(scene));
        }

        public void PlaySound(Enum.BGMScene sound, Enum.BGMScene endToBGM)
        {
            _bgmView.PlaySound(_bgmModel.GetSceneBGM(sound));

            _bgmView.OnCompleteSound.Subscribe(_ =>
            {
                _bgmView.PlayLoopBGM(_bgmModel.GetSceneBGM(endToBGM));
            });

        }

        public void PlayGameOverSound()
        {
            StartCoroutine(PlayGameOver());

            _bgmView.OnCompleteSound.Subscribe(_ =>
            {

            });
        }

        private IEnumerator PlayGameOver()
        {
            _bgmView.StopSound();
            yield return new WaitForSeconds(1.5f);

            _bgmView.PlaySound(_bgmModel.GetSceneBGM(Enum.BGMScene.GameOver));

        }

    }
}