using UnityEngine;

namespace Naichilab.Scripts.Internal
{
    [CreateAssetMenu(menuName = "GyazoUploader/Create GyazoSetting Asset")]
    public class GyazoSetting : ScriptableObject
    {
        public string GyazoAccessToken;
    }
}