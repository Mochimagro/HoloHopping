using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using DG.Tweening;

namespace Legacy
{
    public class StarRush : Item
    {

        private void Start()
        {
            base.triggerObserver.Subscribe(_ =>
            {
                itemManager.StarRush();
                SetScoreText("STAR RUSH!!");
            });

        }
    }
}