using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHolder : MonoBehaviour
{
    public static GameObject characterHoldingBomb;

    [SerializeField] LevelSO level;

    [HideInInspector] public bool isHoldingBomb;
    Bomb levelBomb;
    GameObject bombGO;

    private void Awake()
    {
        levelBomb = level.bombPrefab;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BombHolder>())
        {
            if (isHoldingBomb)
            {
                DisableBomb();
                collision.gameObject.GetComponent<BombHolder>().HoldBomb();
            }
        }
    }
    public void DisableBomb()
    {
        if (bombGO)
        {
            //Destroy(bombGO);
            isHoldingBomb = false;
        }

    }
    public void HoldBomb()
    {
        if (!Bomb.currentBomb)
        {
            bombGO = Instantiate(levelBomb.gameObject, transform, false);
        }
        else
        {
            bombGO = Bomb.currentBomb.gameObject;
            bombGO.transform.SetParent(transform, false);
        }

        bombGO.transform.localPosition = Vector3.zero + transform.up;

        Invoke("SetHoldingBomb", 0.5f);
        characterHoldingBomb = gameObject;
    }

    void SetHoldingBomb()
    {
        isHoldingBomb = true;
    }

    private void OnDestroy()
    {
        LevelManager.bombHolderPlayers.Remove(this);
    }
}
