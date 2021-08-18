using UnityEngine;

public class BombHolder : MonoBehaviour
{
    public enum State
    {
        Idle,
        IdlewithBomb,
        Running,
        RunningwithBomb
    }

    public static GameObject characterHoldingBomb;

    [SerializeField]    LevelSO level;
    [SerializeField]    State state;
    [SerializeField]    Transform bombPlace;
    [SerializeField]    Animator anim;
    [SerializeField]    Material currentMaterial;
    [HideInInspector]   public bool isHoldingBomb;
    
    float       delay;
    Bomb        levelBomb;
    GameObject  bombGO;

    static float lastCollisionTime;
    private void Awake()
    {
        levelBomb = level.bombPrefab;
        lastCollisionTime = Time.time;
    }
    private void OnCollisionEnter(Collision collision)
    {
        delay = Time.time - lastCollisionTime;
        if (delay >= 1 && collision.gameObject.GetComponent<BombHolder>())
        {
            if (isHoldingBomb)
            {
                DisableBomb();

                collision.gameObject.GetComponent<BombHolder>().HoldBomb();

                lastCollisionTime = Time.time;
            }
        }
    }
    private void OnDestroy()
    {
        LevelManager.Instance.bombHolderPlayers.Remove(this);
    }

    #region Helper Functions
    public Material GetMaterial() => currentMaterial;
    public void SetState(State newState)
    {
        state = newState;
        anim.SetTrigger(state.ToString());
    }
    public void DisableBomb()
    {
        if (bombGO)
        {
            isHoldingBomb = false;

            if (state == State.IdlewithBomb) state = State.Idle;
            else if (state == State.RunningwithBomb) state = State.Running;
            anim.SetTrigger(state.ToString());
        }

    }
    public void HoldBomb()
    {
        if (!Bomb.currentBomb)
        {
            bombGO = Instantiate(levelBomb.gameObject, bombPlace, false);
        }
        else
        {
            bombGO = Bomb.currentBomb.gameObject;
            bombGO.transform.SetParent(bombPlace, false);
        }

        bombGO.transform.localPosition = Vector3.zero;

        if (state == State.Idle) state = State.IdlewithBomb;
        else if (state == State.Running) state = State.RunningwithBomb;
        anim.SetTrigger(state.ToString());

        isHoldingBomb = true;
        characterHoldingBomb = gameObject;

        AudioManager.Instance.PlayClip(AudioManager.GameClips.BombPick);
    }
    #endregion
}
