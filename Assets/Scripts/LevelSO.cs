using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level Settings/Level")]

public class LevelSO : ScriptableObject
{
    public int AICount;
    public GameObject[] AIPrefabs;
    public GameObject bombPrefab;
}
