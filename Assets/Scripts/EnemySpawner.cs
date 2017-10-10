using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Enemy enemyPrefab;

    public int levelNum;

    //Needs to change when player script is added
    Vector2 playerPos;

    //small = faster
    //higher = slower
    const float spawnRateModifier = 1;

    //delay so player can get a grip of the level
    int basicStartSpawnDelay = 5;

	// Use this for initialization
	void Start () {
        //get level number here

        //Speed is based off of level num and the modifier.
		float spawnRate = (1.0f/GameManager.level) * spawnRateModifier;

        //spawn enemies at a constant speed. 
        InvokeRepeating("SpawnEnemy", basicStartSpawnDelay, spawnRate);

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    //Spawns enemies on platforms 20 units away from the player
    void SpawnEnemy()
    {
        //Needs to change
        playerPos = new Vector2(100, 100);

        bool isDone = false;
        Vector2 point = Vector2.zero;
        while (!isDone)
        {
            point = Vector2.zero;

            PlatformBase platform = Level.platforms[Random.Range(0, Level.platforms.Length)];

            

            

            point = platform.Pivot + new Vector2(Random.Range(1, platform.Width - 1), 0)+new Vector2(0, 1.1f);

            print(point);

            //check player distance
            if (Vector2.Distance(point, playerPos) >= 20)
            {
                isDone = true;
            }


        }

        //spawn enemy
        //will change when more enemies are added
        Instantiate(enemyPrefab, point, Quaternion.identity);




    }
}
