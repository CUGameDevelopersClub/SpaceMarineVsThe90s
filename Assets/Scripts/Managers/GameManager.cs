using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject playerPrefab;
    public LevelMaker levelManager;
    public int chaos = 0;
    public float maxChaos;
    public int level = 0;
    public int maxEnemies;

    private EnemySpawner enemySpawner;

    private void Awake() {
        DontDestroyOnLoad(transform.gameObject);
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        NextLevel();
    }

    private void Update() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player.transform.position.y <= -500) {
            ResetPlayer();
        }
    }

    //Ready for the next level?
    public void NextLevel() {
		chaos = 0;
        level++;
        maxChaos = level * 50;
        maxEnemies = level * 10;

        //Destroy player and enemies
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            Destroy (player);

        enemySpawner.ClearEnemies();
        enemySpawner.StopSpawning();

        //GENERATE NEW LEVEL
        LevelGenerator.GenerateLevel(levelManager.levelPrototype);
        levelManager.CreateLevel();

        enemySpawner.BeginSpawning();

        //Set player position
        Instantiate(playerPrefab, Level.StartGate.position, Quaternion.identity);
    }

    //Adds points
    public void AddChaos(int pts) {
        chaos += pts;
    }

    public void ResetPlayer () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        player.transform.position = Level.StartGate.position;
    }
}