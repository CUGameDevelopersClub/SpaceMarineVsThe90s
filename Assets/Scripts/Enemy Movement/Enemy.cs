using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * All Enemies should inherit from this
 * 
 * Must call setUp() on start up
 * 
*/


public class Enemy : MonoBehaviour
{

    public int health;
    public int tier;
    public float speed;

    //Walking: collides with walls
    //Flying: does not collide with walls
    //might add more
    public enum EnemyType { Walking, Flying};
    public EnemyType enemyType;

    public Player player;
    public EnemySpawner enemySpawner;

    Rigidbody2D rigidBody2D;

    //basic enemy setup
    //Must be called in the start function
    public void EnemySetUp(int health, EnemyType enemyType, float speed, int tier)
    {
        this.health = health;
        this.enemyType = enemyType;
        this.tier = tier;
        this.speed = speed;

        rigidBody2D = GetComponent<Rigidbody2D>();
    }


    
    //call in fixedUpdate
    public void FlyingMovement()
    {

        //rotation
        //may want smooth rotation
        Vector2 distance = new Vector2(player.transform.position.x - transform.position.x,
            player.transform.position.y - transform.position.y);
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //movement
        rigidBody2D.MovePosition(transform.position + (transform.up * speed));


    }
    
    
    
    //call in fixedUpdate
    //Inputs need to include overall position
    //debug to true if you want to see raycast lines in scene manager
    public void GroundMovement(Vector3 bottomRight, Vector3 bottomLeft, bool debug)
    {
        if (CanMove(bottomRight, bottomLeft, debug))
        {
            if (player.transform.position.x > transform.position.x)
            {
                rigidBody2D.MovePosition(transform.position + new Vector3(speed, 0, 0));
            }
            else if (player.transform.position.x < transform.position.x)
            {
                rigidBody2D.MovePosition(transform.position + new Vector3(-speed, 0, 0));
            }
        }
        
    }
	
    //rayCast downward to see if it still has a platform below
    bool CanMove(Vector3 bottomRight, Vector3 bottomLeft, bool debug)
    {
        //draw debug lines
        if (debug)
        {
            Debug.DrawRay(bottomRight, -transform.up, Color.red, 0.2f);
            Debug.DrawRay(bottomLeft, -transform.up, Color.red, 0.2f);
        }

        //cast rays
        RaycastHit2D hitRight = Physics2D.Raycast(bottomRight, -transform.up, 0.1f);
        RaycastHit2D hitLeft = Physics2D.Raycast(bottomLeft, -transform.up, 0.1f);

        //check if it hit something
        if (hitRight.collider != null && hitLeft.collider != null)
        {
            //check if it hit a platform
            if (hitRight.collider.gameObject.GetComponent<Platform>() != null &&
                hitLeft.collider.gameObject.GetComponent<Platform>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        
    }

    //checks the health. If lower then 0 destroy
    public void checkHealth()
    {
        if (health <= 0)
        {

            enemySpawner.DestroyedEnemy(tier);

            Destroy(gameObject);
        }
    }
}
