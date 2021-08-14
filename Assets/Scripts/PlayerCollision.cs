using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    BombHolder bombHolder;
    private void Awake()
    {
        bombHolder = GetComponent<BombHolder>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<BombHolder>())
        {
            if(bombHolder.holdsBomb)
            {
                bombHolder.DisableBomb();
                collision.gameObject.GetComponent<BombHolder>().HoldBomb();
            }
            else if(collision.gameObject.GetComponent<BombHolder>().holdsBomb)
            {
                collision.gameObject.GetComponent<BombHolder>().DisableBomb();
                bombHolder.HoldBomb();
            }
        }
    }
}
