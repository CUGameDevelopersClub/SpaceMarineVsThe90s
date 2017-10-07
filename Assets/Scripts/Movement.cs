using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed;

    Rigidbody2D rigidBody2D;


    //This must be called in the start function
    public void MovementSetUp(float speed)
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        this.speed = speed;
    }

    //This must be called in the fixed update function
    public void MoveRight()
    {
        rigidBody2D.MovePosition(transform.position + new Vector3(speed, 0, 0));
    }

    //This must be called in the fixed update function
    public void MoveLeft()
    {
        rigidBody2D.MovePosition(transform.position + new Vector3(-speed, 0, 0));
    }
	
}
