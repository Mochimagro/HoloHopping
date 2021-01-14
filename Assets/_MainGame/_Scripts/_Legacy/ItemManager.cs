using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UniRx;
using DG.Tweening;

namespace Legacy
{
    public class ItemManager : MonoBehaviour
    {
        //[SerializeField] private Item scoreItem = null;
        [SerializeField] private List<Item> scoreItemList = new List<Item>();
        [SerializeField] private List<Item> specialItemList = new List<Item>();
        [SerializeField] private List<Item> characterItemList = new List<Item>();
        [SerializeField] private List<Item> starRushItem = new List<Item>();
        [NonSerialized] public GameManager gameManager = null;

        public Subject<int> itemScoreSubject = new Subject<int>();
        public Subject<Unit> characterItemSubject = new Subject<Unit>();

        public Item CreateScoreItem(Item scoreItem)
        {
            var item = Instantiate(scoreItem, RandomPosition(), scoreItem.transform.rotation);

            item.BornAnimation();
            item.triggerObserver.Subscribe(
                _ =>
                {
                    itemScoreSubject.OnNext(item.score);
                    item.EffectBlight();
                    Destroy(item.gameObject);
                }
            );

            return item;
        }

        public Item CreateScoreItem()
        {
            return CreateScoreItem(scoreItemList[0]);
        }

        public Item CreateSpecialItem()
        {
            var pos = RandomPosition();
            pos.y = UnityEngine.Random.Range(8f, 12f);
            pos.x = 13f;

            int itemIndex = UnityEngine.Random.Range(0, specialItemList.Count);

            var item = Instantiate(specialItemList[itemIndex], pos, specialItemList[itemIndex].transform.rotation);

            item.itemManager = this;
            item.triggerObserver.Subscribe(
                _ =>
                {
                    itemScoreSubject.OnNext(item.score);
                    item.EffectBlight();
                    Destroy(item.gameObject);
                }
            );

            return item;
        }

        public void CreateCharacterItem()
        {
            var item = Instantiate(characterItemList[0], RandomPosition(), characterItemList[0].transform.rotation);

            item.BornAnimation();

            item.triggerObserver.Subscribe(col =>
            {
                characterItemSubject.OnNext(Unit.Default);
                item.EffectBlight();
                Destroy(item.gameObject);
            });

        }


        private Vector3 RandomPosition()
        {
            Vector3 tmp = Vector3.zero;
            tmp.x = UnityEngine.Random.Range(-11.0f, 11.0f);
            tmp.y = UnityEngine.Random.Range(6.0f, 11.5f);
            return tmp;
        }

        public void StarRush()
        {
            Observable.Interval(TimeSpan.FromSeconds(0.4f)).Take(50)
            .Subscribe(_ =>
            {

                var item = CreateScoreItem(starRushItem[0]);
                item.transform.DOMoveY(18f, 0);

                item.tweenSequence.Append(item.transform.DOMove(new Vector3(UnityEngine.Random.Range(-13f, 13f), 0f, 0f), 1.8f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Destroy(item.gameObject);
                }))
                ;
            }).AddTo(this);
        }
    }
}