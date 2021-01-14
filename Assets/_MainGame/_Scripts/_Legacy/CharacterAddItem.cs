using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Legacy
{
    public class CharacterAddItem : Item
    {
        private void Start()
        {
            base.triggerObserver.Subscribe(_ =>
            {
                itemManager.characterItemSubject.OnNext(Unit.Default);
                SetScoreText("NEW CHARACTER!!");
            });
        }
    }
}