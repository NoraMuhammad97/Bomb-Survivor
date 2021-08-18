using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level Settings/Level")]

public class LevelSO : ScriptableObject
{
    public int AICount;
    public GameObject[] AIPrefabs;
    public Bomb bombPrefab;
    [Header("Bomb Timer in Seconds")]
    public int bombTimerCount;
}
