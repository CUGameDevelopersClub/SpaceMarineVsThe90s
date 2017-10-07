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


public class Enemy : Movement {

    public int health;

    //Walking: collides with walls
    //Flying: does not collide with walls
    //might add more
    public enum EnemyType { Walking, Flying};
    public EnemyType enemyType;

    //basic enemy setup
    //Must be called in the start function
    public void EnemySetUp(int health, EnemyType enemyType, float speed)
    {
        this.health = health;
        this.enemyType = enemyType;

        MovementSetUp(speed);
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
