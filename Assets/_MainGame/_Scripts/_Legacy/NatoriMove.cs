using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using DG.Tweening;

namespace Legacy
{
    public class NatoriMove : MonoBehaviour
    {
        [SerializeField] private Transform LTop = null, RTop = null, LBottom = null, RBottom = null;
        [SerializeField] private Transform natoriHead = null;
        private Animator animator => GetComponent<Animator>();
        private List<GameObject> missList = new List<GameObject>();

        public void AddMiss(GameObject missCharacter)
        {
            missList.Add(missCharacter);

            if (missList.Count == 1)
            {
                MissMove(missList[0]);
            }
        }

        public void MissMove(GameObject missCharacter)
        {
            animator.SetBool("Carry", false);
            Sequence tweenSequence = DOTween.Sequence();

            if (missCharacter.transform.position.x >= 0)
            {

                tweenSequence.Append(
                    transform.DOPath(
                    new Vector3[]{
                    LBottom.transform.position,
                    LTop.transform.position,
                    missCharacter.transform.position,
                    },
                    3f
                )
                .SetEase(Ease.Linear)
                .SetLookAt(0.0f)
                )
                .SetLoops(2, LoopType.Yoyo)
                .AppendCallback(() =>
                {
                    animator.SetBool("Carry", true);
                    missCharacter.transform.position = natoriHead.transform.position;
                    missCharacter.transform.SetParent(natoriHead);
                })
                .OnComplete(() =>
                {
                    missList.Remove(missCharacter);
                    Destroy(missCharacter);
                    if (missList.Count >= 1)
                    {
                        MissMove(missList[0]);
                    }
                });

            }
            else
            {
                tweenSequence.Append(
                    transform.DOPath(
                        new Vector3[]{
                        RBottom.transform.position,
                        RTop.transform.position,
                        missCharacter.transform.position,
                        },
                        3f
                        )
                    .SetEase(Ease.Linear)
                    .SetLookAt(0.0f)
                    )
                    .SetLoops(2, LoopType.Yoyo)
                    .AppendCallback(() =>
                        {
                            animator.SetBool("Carry", true);
                            missCharacter.transform.position = natoriHead.transform.position;
                            missCharacter.transform.SetParent(natoriHead);
                        })
                        .OnComplete(() =>
                            {
                                missList.Remove(missCharacter);
                                Destroy(missCharacter);
                                if (missList.Count >= 1)
                                {
                                    MissMove(missList[0]);
                                }
                            }
                        );
            }
        }
    }
}