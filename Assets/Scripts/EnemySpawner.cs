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
        levelNum = 1;

        //Speed is based off of level num and the modifier.
        float spawnRate = (1.0f/Mathf.Log10(levelNum + 1)) * spawnRateModifier;

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
        playerPos = new Vector2(Level.levelArray.GetLength(0) / 2, Level.levelArray.GetLength(1) / 2);

        bool isDone = false;
        Vector2 point = Vector2.zero;
        while (!isDone)
        {
            point = Vector2.zero;
            //get point
            for (int x = 0; x < Level.levelArray.GetLength(0); x++)
            {
                for (int y = 0; y < Level.levelArray.GetLength(1); y++)
                {
                    if (Level.levelArray[x, y] == 1)
                    {
                        //random for randomness of spawning
                        if (Random.value < 0.001f)
                        {
                            point = new Vector2(x, y + 1);
                            isDone = true;
                        }
                    }
                }
            }

            //if point is found
            if (isDone)
            {
                //check player distance
                if (Vector2.Distance(point, playerPos) < 20)
                {
                    isDone = false;
                }
            }



        }

        //spawn enemy
        //will change when more enemies are added
        Instantiate(enemyPrefab, point, Quaternion.identity);




    }
}
