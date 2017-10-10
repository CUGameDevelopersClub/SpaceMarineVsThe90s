﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameManager gameManager;
    public Enemy enemyPrefab;

    public int currentEnemies;
    //Needs to change when player script is added
    private Transform player;

    //small = faster
    //higher = slower
    const float spawnRateModifier = 1;

    //delay so player can get a grip of the level
    int basicStartSpawnDelay = 5;

    private void Awake() {
        DontDestroyOnLoad(transform.gameObject);
       
    }

    public void BeginSpawning () {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        //Speed is based off of level num and the modifier.
        float spawnRate = (1.0f/ gameManager.level) * spawnRateModifier;

        //spawn enemies at a constant speed. 
        InvokeRepeating("SpawnEnemy", basicStartSpawnDelay, spawnRate);
    }

    public void StopSpawning () {
        CancelInvoke("SpawnEnemy");
    }

    //Spawns enemies on platforms 20 units away from the player
    void SpawnEnemy() {
        if (currentEnemies >= gameManager.maxEnemies)
            return;
        
        //Needs to change
        player = GameObject.FindGameObjectWithTag("Player").transform;

        bool isDone = false;
        Vector2 point = Vector2.zero;

        while (!isDone) {
            PlatformBase platform = Level.platforms[Random.Range(0, Level.platforms.Length)];

            point = platform.Pivot + new Vector2(Random.Range(1, platform.Width), 1.1f);

            //check player distance
            if (Vector2.Distance(point, player.position) >= 20) {
                isDone = true;
            }
        }

        //spawn enemy
        //will change when more enemies are added
        Instantiate(enemyPrefab, point, Quaternion.identity, transform);

        currentEnemies++;
    }

    public void ClearEnemies () {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in transform)
            children.Add(child.gameObject);

        children.ForEach(x => Destroy(x));

        currentEnemies = 0;
    }

    public void DestroyedEnemy(int points) {
        currentEnemies--;
        //add chaos

        gameManager.AddChaos(points);
    }
}
