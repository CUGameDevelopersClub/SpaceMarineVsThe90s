using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : ObjectMovement
{
    public int playerDamage; // amount of health to subtract from player upon hit

    public int moveSpeed = 15; // per second
    public int computerDirection;
    Vector3 moveDirection = new Vector3(-1, 0, 0);

    bool isMovingLeft = false;

    int direction; // choose random direction

    private void Start()
    {

    }

    void Update()
    {
        direction = Random.Range(1, 3);

        // If moving right and position is -100
        // Move in a random direction
        // If moving right, set moving left to false
        // If moving left, set moving left to true
        if (!isMovingLeft && transform.localPosition.x <= -100 && direction == 2)
        {
            moveDirection = new Vector3(1, 0, 0);
            isMovingLeft = (direction == 2) ? true : false;
        }
        // If moving left and position is 100
        // Move in a random direction
        else if (isMovingLeft && transform.localPosition.x >= 100 && direction == 1)
        {
            moveDirection = new Vector3(-1, 0, 0);
            isMovingLeft = (direction == 1) ? true : false;
        }

        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
    }
}
