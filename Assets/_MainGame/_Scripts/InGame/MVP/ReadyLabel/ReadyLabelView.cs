using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

namespace HoloHopping.View
{

    public class ReadyLabelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label = null;

        public string SetLabelText
        {
            set { _label.text = value; }
        }

    }
}