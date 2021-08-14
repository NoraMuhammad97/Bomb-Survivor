using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] LevelSO level;
    [SerializeField] PlayerJoystickController mainPlayer;
    [SerializeField] GameObject[] AIRandomPlaces;

    List<BombHolder> players;
    GameObject randomAIPrefab;
    //GameObject AIParent; //in hierarchy
    private void Awake()
    {
        players = new List<BombHolder>();

        InstantiateRandomAIPlayers();

        InstantiateBombForRandomPlayer();
    }
    private void Start()
    {
        
    }

    void InstantiateRandomAIPlayers()
    {
        players.Add(mainPlayer.GetComponent<BombHolder>());

        for (int i = 0; i < level.AICount; i++)
        {
            randomAIPrefab = level.AIPrefabs[Random.Range(0, level.AIPrefabs.Length)];
            players.Add(Instantiate(randomAIPrefab, AIRandomPlaces[i].transform.position, Quaternion.identity).GetComponent<BombHolder>());
        }
    }

    void InstantiateBombForRandomPlayer()
    {
        int playerIndex = Random.Range(0, players.Count);

        players[playerIndex].HoldBomb();
    }
}
