using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legacy
{
    public class PlayerMove : MonoBehaviour
    {
        private Animator animator => GetComponent<Animator>();
        [SerializeField] private float speed = 0.05f;
        [SerializeField] SkinnedMeshRenderer skinnedMesh = null;
        [SerializeField] HoppingHelmet helmet = null;
        //[SerializeField] private Transform headRoot = null;

        private bool moveable = true;

        private void Start()
        {
            //helmet.transform.SetParent(headRoot.transform, false);
        }

        Vector3 motion = Vector3.zero;
        private void FixedUpdate()
        {

            motion = transform.position;
            var x = Input.GetAxisRaw("Horizontal");

            animator.SetFloat("move", Mathf.Abs(x));

            if (moveable)
            {
                motion.x += -x * speed;
                motion.x = Mathf.Clamp(motion.x, -12.0f, 12.0f);
                transform.position = motion;
                motion = transform.position;
            }
        }
        public void ChargeStart()
        {
            animator.SetBool("Charge", true);
            moveable = false;
            skinnedMesh.SetBlendShapeWeight(0, 100.0f);
        }



        public void ChargeEnd(bool actionSuccess)
        {
            animator.SetBool("Charge", false);
            moveable = true;
            skinnedMesh.SetBlendShapeWeight(0, 0.0f);
            if (actionSuccess)
            {
                var fx = Instantiate(helmet.FX_HighJump, helmet.transform.position, helmet.FX_HighJump.transform.rotation);
                fx.gameObject.SetActive(true);
            }
            else
            {
                var fx = Instantiate(helmet.FX_NormalJump, helmet.transform.position, helmet.FX_NormalJump.transform.rotation);
                fx.gameObject.SetActive(true);
            }
        }


    }
}