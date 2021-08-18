using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] LevelSO level;
    [SerializeField] PlayerJoystickController mainPlayer;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject[] AIRandomPlaces;

    public List<BombHolder> bombHolderPlayers;
    public bool GameIsPaused;

    GameObject randomAIPrefab;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(this);

        bombHolderPlayers = new List<BombHolder>();
        GameIsPaused = true;
    }
    private void Start()
    {
        InstantiateRandomAIPlayers();

        InstantiateBombForRandomPlayer();
    }

    void InstantiateRandomAIPlayers()
    {
        bombHolderPlayers.Add(mainPlayer.GetComponent<BombHolder>());

        for (int i = 0; i < level.AICount; i++)
        {
            randomAIPrefab = level.AIPrefabs[Random.Range(0, level.AIPrefabs.Length)];
            bombHolderPlayers.Add(Instantiate(randomAIPrefab, AIRandomPlaces[i].transform.position, Quaternion.identity).GetComponent<BombHolder>());
        }
    }

    public void InstantiateBombForRandomPlayer()
    {
        int playerIndex = Random.Range(0, bombHolderPlayers.Count);

        if(bombHolderPlayers.Count == 1 && bombHolderPlayers[playerIndex].CompareTag("Player"))
        {
            Instantiate(winPanel, GameObject.Find("Canvas").transform, false);

            AudioManager.Instance.StopClip(AudioManager.GameClips.Background);
            AudioManager.Instance.PlayClip(AudioManager.GameClips.Win);
        }
        else if (bombHolderPlayers.Count > 0)
        {
            bombHolderPlayers[playerIndex].HoldBomb();
        }
    }
    public void PlayerLoses()
    {
        Instantiate(gameoverPanel, GameObject.Find("Canvas").transform, false);
        GameIsPaused = true;
    }
    private void OnDestroy()
    {
        bombHolderPlayers.Clear();
    }
}
