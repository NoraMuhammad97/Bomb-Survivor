using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Transform handle;
    public void DisableTutorial()
    {
        LevelManager.Instance.EnablePlayersMovement();

        Destroy(transform.parent.GetComponent<Animator>());

        Rect handleRect = handle.GetComponent<RectTransform>().rect;
        handleRect.Set(0, 0, handleRect.width, handleRect.height);

        Destroy(gameObject);
    }
}
