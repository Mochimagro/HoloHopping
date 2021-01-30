using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Model
{

    public class GameSystemModel
    {
        private IntReactiveProperty _leaveHoppingCharacters = new IntReactiveProperty();

        public void AddLeaveCount()
        {
            _leaveHoppingCharacters.Value++;
        }

        public void ReduceLeaveCount ()
        {
            _leaveHoppingCharacters.Value--;
        }

        public IObservable<int> AllCharacterMiss => _leaveHoppingCharacters.Where(value => value <= 0);

    }
}