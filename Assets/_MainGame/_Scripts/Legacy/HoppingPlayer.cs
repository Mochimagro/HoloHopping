using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using DG.Tweening;

namespace Legacy
{
    public class HoppingPlayer : MonoBehaviour
    {
        private Collider col => GetComponent<BoxCollider>();
        private Rigidbody rb => GetComponent<Rigidbody>();
        private Animator animator => GetComponent<Animator>();
        private bool startedPlayer = false;
        public Subject<Unit> missSubject = new Subject<Unit>();
        public Subject<int> hoppingSubject = new Subject<int>();
        public PlayerMove playerMove = null;
        public GameManager gameManager = null;

        // 300 500
        [SerializeField] float jumpPower = 500f;

        private void Update()
        {
            animator.SetFloat("velocity", rb.velocity.y);

            if (Input.GetButtonDown("Jump"))
            {
                ActionButton();
            }
        }


        float dis = 0;
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Board"))
            {
                if (transform.position.y < other.transform.position.y) { return; }
                rb.velocity = Vector3.zero;
                dis = transform.position.x - other.transform.position.x;
                hoppingSubject.OnNext(multiItemCount);
                multiItemCount = 0;
                pushFrame = Time.frameCount;
                playerMove.ChargeStart();
                animator.SetTrigger("Charge");
                //StartCoroutine(ActionButton());
            }
            else if (other.gameObject.CompareTag("Ground"))
            {
                if (startedPlayer)
                {
                    animator.SetTrigger("Miss");
                    col.enabled = false;
                    rb.useGravity = false;
                    rb.isKinematic = true;
                    missSubject.OnNext(Unit.Default);
                }


            }
        }

        int multiItemCount = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Score"))
            {
                multiItemCount += multiItemCount + 1;
            }
            else if (other.CompareTag("ActionArea"))
            {
                ableActionButton = false;
                isActionArea = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ActionArea"))
            {
                isActionArea = false;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            startedPlayer = true;
        }

        private bool isActionArea = false;
        private bool ableActionButton = false;
        private int actionButtonFrame = 0;
        private int pushFrame = 0;

        private void ActionButton()
        {
            if (isActionArea)
            {
                if (!ableActionButton)
                {
                    actionButtonFrame = Time.frameCount;
                    ableActionButton = false;
                }
            }
        }

        // private IEnumerator ActionButton()
        // {
        //     animationEnd = false;
        //     bool succesAction = false;
        //     playerMove.ChargeStart();
        //     animator.SetTrigger("Charge");

        //     while (true)
        //     {

        //         if (Input.GetKeyDown(KeyCode.Space))
        //         {
        //             succesAction = true;
        //             //yield break;
        //         }


        //         if (animationEnd)
        //         {
        //             if (succesAction)
        //             {
        //                 PowerJump(1.3f);
        //                 yield break;
        //             }
        //             PowerJump(1.0f);
        //             yield break;
        //         }
        //         yield return null;
        //     }

        // }

        // private bool animationEnd = false;

        /// <summary>
        /// Animation メソッド
        /// </summary>
        public void ChargeEnd()
        {
            //animationEnd = true;

            if (Math.Abs(pushFrame - actionButtonFrame) <= 30)
            {
                PowerJump(1.3f);
                return;
            }
            PowerJump(1.0f);
        }

        public void StartJump(int side)
        {
            transform.DORotate(new Vector3(0f, -60f * side, 0f), 0);
            transform.DOMoveX(side * 7.15f, 1.5f).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                rb.AddForce(new Vector3(-side * 200f, 500f, 0f));
                animator.SetTrigger("Start");
                rb.useGravity = true;
                col.enabled = true;
            });

        }

        private void PowerJump(float power)
        {

            rb.AddForce(new Vector3(dis * 200f * power, jumpPower * power, 0));
            playerMove.ChargeEnd(power > 1.1f);
        }

    }
}