using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacter : MonoBehaviour
{
    BombHolder bombHolder;
    NavMeshAgent myAgent;
    void Awake()
    {
        if (GetComponent<BombHolder>())
            bombHolder = GetComponent<BombHolder>();
        if (GetComponent<NavMeshAgent>())
            myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (bombHolder && LevelManager.bombHolderPlayers != null)
        {
            if (bombHolder.isHoldingBomb)
            {
                SeekNearestPlayer();
            }
            else
            {
                HideFromBomb();
            }
        }
    }
    void SeekNearestPlayer()
    {

        List<BombHolder> holders = LevelManager.bombHolderPlayers;
        float minDis = float.MaxValue;
        Vector3 target = transform.position;

        for (int i = 0; i < holders.Count; i++)
        {
            if (holders[i].gameObject != gameObject)
            {
                float distance = Vector3.Distance(transform.position, holders[i].transform.position);
                if (distance < minDis && distance > 1.5f)
                {
                    minDis = Vector3.Distance(transform.position, holders[i].transform.position);
                    target = holders[i].transform.position;
                }
            }
        }

        myAgent.isStopped = false;
        myAgent.SetDestination(target);
    }

    void HideFromBomb()
    {
        myAgent.isStopped = true;
    }
}
