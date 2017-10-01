using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance = null;
    private int difficultyLevel;
    private int playerHealth;

    private int level = 1;
    private bool isEnemyMoving;
    private bool doingSetup;

    public Transform spawnPoint;

    private void Awake()
    {

    }


    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void initGame()
    {

    }

    protected virtual void genWorld() { }

    protected virtual void spawnWorld() { }

    protected virtual void checkLife() { }
}
