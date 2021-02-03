using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Presenter
{
    using Model;
    using View;
    public class CharacterSelectPresenter : MonoBehaviour
    {
        [SerializeField] private CharacterSelectView _characterSelectView = null;
        private CharacterSelectModel _characterSelectModel = null;
        [SerializeField] private Data.CharacterList _characterDataList = null;
        [SerializeField] private Arbor.GlobalParameterContainer _parameterContainer = null;


        public void Init()
        {

            this.gameObject.SetActive(true);

            _characterSelectModel = new CharacterSelectModel(new Entity.CharacterEntityList(_characterDataList));
            _characterSelectView.Init();


            Bind();
        }

        private void Bind()
        {
            _characterSelectView.OnSelectLeft.TakeUntilDisable(this).Subscribe(_ =>
            {
                _characterSelectModel.SetIndexAdd = -1;
            });

            _characterSelectView.OnSelectRight.TakeUntilDisable(this).Subscribe(_ =>
            {
                _characterSelectModel.SetIndexAdd = +1;
            });

            _characterSelectModel.OnChangeIndex.TakeUntilDisable(this).Subscribe(value =>
            {
                _characterSelectView.SetTextCharacterName = _characterSelectModel.GetSelectCharacterName;
                _characterSelectView.ShowSelectCharacterObject(_characterSelectModel.GetSelectCharacterObject);

            });

            _characterSelectView.OnDecision.TakeUntilDisable(this).Subscribe(_ =>
            {
                _parameterContainer.container.SetString("SelectCharacter", _characterSelectModel.GetSelectCharacterName);
                _characterSelectView.MoveSelectCharacter();
            });

        }


        public void Kill()
        {
            //this.gameObject.SetActive(false);
        }
    }
}