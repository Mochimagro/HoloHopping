using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
using System;
using DG.Tweening;
using TMPro;

namespace Legacy
{
    [System.Serializable]
    public class AudioList
    {
        public AudioClip playBGM = null;
        public AudioClip bombSE = null;
        public AudioClip missBGM = null;
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<HoppingPlayer> hoppingPlayers = new List<HoppingPlayer>();
        [SerializeField] private List<HoppingPlayer> activePlayer = new List<HoppingPlayer>();

        [SerializeField] private GameObject ControllerCharacter = null;
        [SerializeField] private ParticleSystem FX_MissBomb = null;
        [SerializeField] private AudioList audioList = null;
        [SerializeField] private NatoriMove natori = null;
        private int score = 0;
        private int starCount = 0;
        // private int hopCount = 0;
        [SerializeField] private TextMeshProUGUI scoreText = null;

        private PlayerMove playerMove => ControllerCharacter.GetComponent<PlayerMove>();
        private ItemManager itemManager => GetComponent<ItemManager>();
        private AudioSource audioSource => GetComponent<AudioSource>();

        private void Start()
        {
            // DebugKeyCode();

            audioSource.loop = true;
            audioSource.clip = audioList.playBGM;
            audioSource.Play();
            itemManager.characterItemSubject.Subscribe(_ =>
            {
                InsertPlayer();
            });

            InsertPlayer();

            itemManager.gameManager = this;

            // アイテム獲得Subject
            itemManager.itemScoreSubject.Subscribe(score =>
            {

            // スコア加算
            int i = this.score;
                this.score += score;
                DOTween.To(
                    () => i,
                    num =>
                    {
                        i = num;
                        scoreText.text = i.ToString();
                    },
                    this.score,
                    0.5f
                ).SetEase(Ease.OutQuad);
            });

            for (int i = 0; i < 5; i++)
            {
                itemManager.CreateScoreItem();
            }

            // 自動生成Subject
            Observable.Interval(TimeSpan.FromSeconds(2.5))
            .Subscribe(_ => itemManager.CreateScoreItem()).AddTo(this);

        }

        private void InsertPlayer()
        {
            var side = playerMove.transform.position.x >= 0 ? -1 : 1;

            var newPlayer = Instantiate(hoppingPlayers[UnityEngine.Random.Range(0, hoppingPlayers.Count)], new Vector3(side * 15f, 1.0f, 0), hoppingPlayers[0].transform.rotation);

            newPlayer.playerMove = playerMove;
            newPlayer.gameManager = this;
            newPlayer.StartJump(side);

            activePlayer.Add(newPlayer);

            newPlayer.missSubject.Subscribe(_ =>
            {

                activePlayer.Remove(newPlayer);
                if (activePlayer.Count == 0)
                {

                    GameOver();
                }
                else
                {
                    audioSource.PlayOneShot(audioList.bombSE);
                }
                var bomb = Instantiate(FX_MissBomb, newPlayer.transform.position, FX_MissBomb.transform.rotation);
                bomb.gameObject.SetActive(true);
                natori.AddMiss(newPlayer.gameObject);
            });

            // ホッピングSubject
            newPlayer.hoppingSubject.Subscribe(count =>
            {
                starCount += count;

                if (starCount >= 10)
                {
                    itemManager.CreateSpecialItem();
                    starCount -= 10;
                }
            });


        }


        private void DebugKeyCode()
        {

            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.R))
                .Subscribe(_ =>
                {
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
                }).AddTo(this);

            Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.T))
            .Subscribe(_ =>
            {
                InsertPlayer();
            }).AddTo(this);
        }

        private IEnumerator PlayMissBGM()
        {
            audioSource.loop = false;
            audioSource.clip = audioList.bombSE;
            audioSource.Play();
            while (true)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = audioList.missBGM;
                    audioSource.Play();

                    yield break;
                }

                yield return null;
            }
        }

        [SerializeField] TextMeshProUGUI gameoverText = null;
        //[SerializeField] TextMeshProUGUI pressKeyText = null;
        private void GameOver()
        {
            // Observable.EveryUpdate()
            //     .Where(_ => Input.GetButtonDown("Jump"))
            //     .Subscribe(_ =>
            //     {
            //         //SceneManager.LoadSceneAsync("Title");
            //         naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
            //     }).AddTo(this);

            StartCoroutine(PlayMissBGM());

            gameoverText.gameObject.SetActive(true);
            gameoverText.DOFade(0, 0);

            Sequence gameoverSeq = DOTween.Sequence();

            DOTweenTMPAnimator gameoverAnimator = new DOTweenTMPAnimator(gameoverText);

            for (int i = 0; i < gameoverAnimator.textInfo.characterCount; i++)
            {

                Vector3 currCharOffset = gameoverAnimator.GetCharOffset(i);

                gameoverSeq.Join(
                    DOTween.Sequence()
                    .Append(gameoverAnimator.DOOffsetChar(i, currCharOffset + new Vector3(0, -60, 0), 0.8f).SetEase(Ease.OutFlash))
                    .Join(gameoverAnimator.DOFadeChar(i, 1, 0.6f))
                    .Join(gameoverAnimator.DOScaleChar(i, 1, 0.6f).SetEase(Ease.OutBack))
                    .SetDelay(0.07f * i)
                );
            }

            gameoverSeq.OnComplete(() =>
            {
                Naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
            });

            // pressKeyText.gameObject.SetActive(true);
            // pressKeyText.DOFade(0, 0);

            // pressKeyText.DOFade(1f, 2.0f)
            // .SetLoops(-1, LoopType.Yoyo)
            // .SetEase(Ease.OutQuad);

        }
    }


}