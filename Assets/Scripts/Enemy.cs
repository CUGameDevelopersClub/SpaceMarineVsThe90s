using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * All Enemies should inherit from this
 * 
 * Must call setUp() on start up
 * 
 * */


public class Enemy : MonoBehaviour {

    public int health;

    //Walking: collides with walls
    //Flying: does not collide with walls
    //might add more
    public enum EnemyType { Walking, Flying};
    public EnemyType enemyType;

    //basic enemy setup
    public void setUp(int health, EnemyType enemytype)
    {
        this.health = health;
        this.enemyType = enemytype;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //checks the health. If lower then 0 destroy
    public void checkHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
