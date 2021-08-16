using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bomb : MonoBehaviour
{
    public static Bomb currentBomb;

    [SerializeField] LevelSO level;
    [SerializeField] GameObject bombTimerPrefab;
    [SerializeField] GameObject gameoverPanel;
    TextMeshProUGUI timerCountText;
    int bombTimerCount;
    GameObject bombTimerGO;
    private void Awake()
    {
        currentBomb = this;

        bombTimerCount = level.bombTimerCount;

        StartBombTimer();
    }
    void StartBombTimer()
    {
        bombTimerGO = Instantiate(bombTimerPrefab, GameObject.Find("Canvas").transform, false);
        timerCountText = bombTimerGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        StartCoroutine(StartCount());
    }

    IEnumerator StartCount()
    {
        for (int i = bombTimerCount; i > 0; i--)
        {
            timerCountText.text = i.ToString();

            yield return new WaitForSeconds(1);
        }

        Explode();
        yield return null;
    }

    void Explode()
    {
        if (transform.parent.gameObject.CompareTag("Player"))
        {
            Instantiate(gameoverPanel, GameObject.Find("Canvas").transform, false);
            LevelManager.Instance.GameIsOver = true;
        }    

        Destroy(transform.parent.gameObject);
    }
    private void OnDestroy()
    {
        Destroy(bombTimerGO);

        currentBomb = null;

        if(!LevelManager.Instance.GameIsOver)
            LevelManager.Instance.InstantiateBombForRandomPlayer();
    }
}
