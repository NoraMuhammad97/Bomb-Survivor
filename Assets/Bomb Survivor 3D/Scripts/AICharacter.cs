using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacter : MonoBehaviour
{
    BombHolder bombHolder;
    NavMeshAgent myAgent;
    GameObject player;
    void Awake()
    {
        if (GetComponent<BombHolder>())
            bombHolder = GetComponent<BombHolder>();
        if (GetComponent<NavMeshAgent>())
            myAgent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player");
    }

    void Update()
    {
        if(!LevelManager.Instance.GameIsOver)
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
                if (distance < minDis /*&& distance > 1.5f*/)
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
        transform.rotation = Quaternion.LookRotation(transform.position - Bomb.currentBomb.transform.parent.transform.position);

        Vector3 runTo = transform.position + transform.forward;

        NavMeshHit hit;    // stores the output in a variable called hit

        NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetNavMeshLayerFromName("Default"));

        myAgent.SetDestination(hit.position);
    }
}
