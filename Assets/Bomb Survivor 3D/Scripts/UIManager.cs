using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] Animator[] SoulImages;

    static int nextIndex;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    private void OnDestroy()
    {
        nextIndex = 0;
    }

    #region Helper Functions
    public void CollectSoul()
    {
        SoulImages[nextIndex].SetTrigger("Collect");
        nextIndex++;
    }
    #endregion
}
