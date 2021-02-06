using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HoloHopping.Component
{

    public class FeverPatternListComponent : MonoBehaviour
    {
        [SerializeField] private List<FeverItemListComponent> _sets = new List<FeverItemListComponent>();
        public List<Vector3> GetRandomPattern
        {
            get
            {
                var target = _sets[UnityEngine.Random.Range(0, _sets.Count)];
                return target.Positions.Select(go => go.transform.position).ToList();
            }
        }
    }
}