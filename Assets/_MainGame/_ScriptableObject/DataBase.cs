using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Data
{

    public struct MenuName
    {
        public struct Format
        {
            public const string DATA = "Data/";
        }

        public struct Attribute
        {
            public const string HOPPING_CHARACTER = "HoppingCharacter/";
            public const string ITEM = "Item/";
            public const string FX = "FX/";
            public const string BGM = "BGM/";
            public const string SE = "SE/";
            public const string CHARACTER = "Character/";
        }

        public struct Type
        {
            public const string PARAMETER = "Parameter";
            public const string LIST = "List";
        }
    }

    public class DataBase : ScriptableObject
    {

    }
}