using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacter : MonoBehaviour
{
    BombHolder      bombHolder;
    NavMeshAgent    myAgent;
    bool            isStopped;
    void Awake()
    {
        if (GetComponent<BombHolder>())
            bombHolder = GetComponent<BombHolder>();
        if (GetComponent<NavMeshAgent>())
            myAgent = GetComponent<NavMeshAgent>();

        transform.forward = -Vector3.forward;
    }

    void Update()
    {
        if(!LevelManager.Instance.GameIsPaused && !isStopped)
        {
            if (bombHolder && LevelManager.Instance.bombHolderPlayers != null)
            {
                if (bombHolder.isHoldingBomb)
                {
                    SeekNearestPlayer();
                    bombHolder.SetState(BombHolder.State.RunningwithBomb);
                }
                else if(Bomb.currentBomb != null)
                {
                    HideFromBomb();
                    bombHolder.SetState(BombHolder.State.Running);
                }
            }
        }
        else
        {
            StopAI();
        }
    }
    public void StopAI()
    {
        myAgent.Stop();

        if (bombHolder.isHoldingBomb)
            bombHolder.SetState(BombHolder.State.IdlewithBomb);
        else
            bombHolder.SetState(BombHolder.State.Idle);
    }
    void SeekNearestPlayer()
    {
        List<BombHolder> holders = LevelManager.Instance.bombHolderPlayers;
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
        //if (myAgent.remainingDistance < 0.5f)
        //{
        //    myAgent.SetDestination(Vector3.zero);
        //}
        //else
        {
            transform.rotation = Quaternion.LookRotation(transform.position - Bomb.currentBomb.transform.root.position);
            Vector3 runTo = transform.position + transform.forward;

            NavMeshHit hit;
            NavMesh.SamplePosition(runTo, out hit, 10, 1 << NavMesh.GetAreaFromName("Default"));

            myAgent.SetDestination(hit.position);
        }
    }
}
