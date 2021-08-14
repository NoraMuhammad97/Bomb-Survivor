using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bomb : MonoBehaviour
{
    [SerializeField] LevelSO level;
    [SerializeField] GameObject bombTimerPrefab;
    TextMeshProUGUI timerCountText;
    int bombTimerCount;
    GameObject bombTimerGO;
    private void Awake()
    {
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

        yield return null;
    }

    private void OnDestroy()
    {
        Destroy(bombTimerGO);
    }
}
