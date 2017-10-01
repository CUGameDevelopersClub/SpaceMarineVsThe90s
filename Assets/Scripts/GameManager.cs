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

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected virtual void gen()
    {

    }

    protected virtual void spawn()
    {

    }

    void initGame()
    {

    }
}
