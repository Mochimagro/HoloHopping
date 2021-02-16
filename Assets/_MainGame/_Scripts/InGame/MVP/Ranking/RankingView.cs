using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;

namespace HoloHopping.View
{
    public class RankingView : MonoBehaviour
    {
        public void Init()
        {


        }

        public void ShowRankingView(int score)
        {
            Naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
        }

    }
}