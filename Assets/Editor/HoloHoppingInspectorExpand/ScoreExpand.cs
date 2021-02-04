/*

#if UNITY_EDITOR
using UnityEditor;

namespace HoloHopping.Editor.InspectorExpand
{
    using Data;

    [CustomEditor(typeof(ItemData))]
    public class ScoreExpand : UnityEditor.Editor
    {
        private ItemData _target;

        private void Awake()
        {
            _target = target as ItemData;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();


            var mode = (Data.ItemMode)EditorGUILayout.EnumPopup("ItemMode", _target.ItemMode);

            if (mode == ItemMode.Score)
            {
            }

        }

    }
}

#endif

*/