using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public void DisableTutorial()
    {
        LevelManager.Instance.GameIsPaused = false;

        Destroy(transform.parent.GetComponent<Animator>());
        Destroy(gameObject);
    }
}
