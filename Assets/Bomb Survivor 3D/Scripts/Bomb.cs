using System.Collections;
using UnityEngine;
using TMPro;
using System;
using Cinemachine;

public class Bomb : MonoBehaviour
{
    public static Bomb currentBomb;

    [SerializeField] LevelSO    level;
    [SerializeField] GameObject bombTimerPrefab;
    [SerializeField] Animator   bombAnim;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject bombCM;

    int             bombTimerCount;
    Animator        bombTimerCountAnim;
    TextMeshProUGUI timerCountText;
    GameObject      bombTimerGO;
    private void Awake()
    {
        currentBomb = this;

        bombTimerCount = level.bombTimerCount;

        StartBombTimer();
    }
    private void OnDestroy()
    {
        Destroy(bombTimerGO);

        currentBomb = null;

        if (!LevelManager.Instance.GameIsPaused)
            LevelManager.Instance.InstantiateBombForRandomPlayer();
    }

    #region Helper Functions
    void StartBombTimer()
    {
        bombTimerGO = Instantiate(bombTimerPrefab, GameObject.Find("Canvas").transform, false);
        bombTimerCountAnim = bombTimerGO.GetComponent<Animator>();
        timerCountText = bombTimerGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>();


        StartCoroutine(StartCount());
    }
    IEnumerator StartCount()
    {
        for (int i = bombTimerCount; i > 0; i--)
        {
            yield return new WaitForSeconds(1);

            TimeSpan time = TimeSpan.FromSeconds(i);
            timerCountText.text = time.ToString(@"mm\:ss");

            if (i <= 10)
            {
                AudioManager.Instance.PlayClip(AudioManager.GameClips.TimerTicking);
                bombTimerCountAnim.SetTrigger("ScaleCount");
            }
        }

        Explode();
        yield return null;
    }
    public void StartAttack()
    {
        AudioManager.Instance.PlayClip(AudioManager.GameClips.BombExplode);
    }
    void Explode()
    {
        if (transform.root.gameObject.CompareTag("Player"))
        {
            transform.root.forward = -Vector3.forward;
            LevelManager.Instance.GameIsPaused = true;

            AudioManager.Instance.StopClip(AudioManager.GameClips.Background);
            AudioManager.Instance.PlayClip(AudioManager.GameClips.Lose);

            CinemachineVirtualCamera vCamera = Instantiate(bombCM).GetComponent<CinemachineVirtualCamera>();
            vCamera.Follow = transform.root;
            vCamera.LookAt = transform.root;
        }
        else
        {
            AICharacter aiCharacter = transform.root.GetComponent<AICharacter>();
            if (aiCharacter != null)
            {
                aiCharacter.StopAI();
            }
        }

        Invoke("Attack", 2);
    }
    void Attack()
    {
        bombAnim.SetTrigger("attack");
    }
    public void DestroyGO()
    {
        explosionPrefab.GetComponent<ParticleSystemRenderer>().material = transform.root.GetComponent<BombHolder>().GetMaterial();
        Destroy(Instantiate(explosionPrefab, transform.parent.position, Quaternion.identity), 1);

        if (transform.root.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.PlayerLoses();
        }
        else
        {
            UIManager.Instance.CollectSoul();
        }

        Destroy(transform.root.gameObject);
    }
    #endregion
}
