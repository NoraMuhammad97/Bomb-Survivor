using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHolder : MonoBehaviour
{
    [SerializeField] LevelSO level;

    [HideInInspector] public bool holdsBomb; 
    GameObject bombPrefab;
    GameObject bomb;
    private void Awake()
    {
        bombPrefab = level.bombPrefab;
    }
    public void HoldBomb()
    {
        bomb = Instantiate(bombPrefab, transform, false);
        bomb.transform.position += transform.up;

        holdsBomb = true;
    }

    public void DisableBomb()
    {
        if (bomb)
        {
            Destroy(bomb);
            holdsBomb = false;
        }
    }
}
