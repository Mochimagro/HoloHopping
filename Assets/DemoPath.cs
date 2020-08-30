using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DemoPath : MonoBehaviour
{

    void Start()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(
            transform.DOPath(
            new Vector3[]{
                new Vector3(0,2,0),
                new Vector3(0,-2,0)
            },
            3f
        )
        .SetEase(Ease.Linear)
        )
        .SetLoops(2, LoopType.Yoyo)
        .AppendCallback(() => Debug.Log("Appenmd"))
        .OnStepComplete(() => Debug.Log("step"))
        .OnComplete(() => Debug.Log("Complete"));


    }
}
