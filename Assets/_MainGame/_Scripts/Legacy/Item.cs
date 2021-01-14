using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Legacy
{
    public class Item : MonoBehaviour
    {
        public ItemManager itemManager = null;

        [SerializeField] private ParticleSystem FX_Blight = null;
        //[SerializeField] private ParticleSystem FX_Born = null;
        [SerializeField] private TextGetScore textGetScore = null;
        [SerializeField] private Color itemColor = Color.white;
        public int score = 0;
        public float lifeTime = 0f;
        private SpriteRenderer meshRenderer => GetComponent<SpriteRenderer>();
        private BoxCollider col => GetComponent<BoxCollider>();

        public IObservable<Collider> triggerObserver => triggerSubject;
        private Subject<Collider> triggerSubject = new Subject<Collider>();
        public Sequence tweenSequence => DOTween.Sequence();

        public void BornAnimation()
        {
            meshRenderer.enabled = false;
            col.enabled = false;
            StartCoroutine(BornSwitch());
        }
        public void EffectBlight()
        {
            Instantiate(FX_Blight, transform.position, Quaternion.identity);

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Character"))
            {
                if (score > 0)
                {
                    SetScoreText(score);
                }
                tweenSequence.Kill();
                triggerSubject.OnNext(other);
            }
        }

        public void SetScoreText(int score)
        {

            var item = Instantiate(textGetScore, transform.position, Quaternion.identity);
            item.StartTextAnimation(score, itemColor);
        }

        public void SetScoreText(string info)
        {
            var item = Instantiate(textGetScore, transform.position, Quaternion.identity);
            item.StartTextAnimation(info, itemColor);

        }

        private IEnumerator BornSwitch()
        {
            yield return new WaitForSeconds(0.5f);
            meshRenderer.enabled = true;
            col.enabled = true;
            StartCoroutine(LifeTime(lifeTime));
        }

        private IEnumerator LifeTime(float lifeTime)
        {
            if (lifeTime <= 0) yield break;

            yield return new WaitForSeconds(lifeTime);
            var aa = new WaitForSeconds(0.1f);

            var i = 0;
            while (true)
            {
                meshRenderer.enabled = !meshRenderer.enabled;

                yield return aa;
                i++;
                if (i >= 20)
                {
                    Destroy(this.gameObject);
                    yield break;
                }
            }
        }

    }
}