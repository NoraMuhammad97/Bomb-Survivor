using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHolder : MonoBehaviour
{
    [SerializeField] LevelSO level;

    [HideInInspector] public bool isHoldingBomb; 
    Bomb levelBomb;
    GameObject bombGO;
    private void Awake()
    {
        levelBomb = level.bombPrefab;
    }
    public void HoldBomb()
    {
        bombGO = Instantiate(levelBomb.gameObject, transform, false);
        bombGO.transform.position += transform.up;

        isHoldingBomb = true;
    }

    public void DisableBomb()
    {
        if (bombGO)
        {
            Destroy(bombGO);
            isHoldingBomb = false;
        }
    }
}
