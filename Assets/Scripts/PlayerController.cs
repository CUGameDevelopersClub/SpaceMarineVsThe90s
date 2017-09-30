using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectMovement {

    public float jumpSpeed = 100; // per second
    public float maxSpeed = 15; // per second

	// Use this for initialization
	void Start ()
    {

	}

    protected override void computeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        
        if ( Input.GetButtonDown("Jump") && isGrounded ) { // for double jumps remove isGrounded
            velocity.y = jumpSpeed;
        } else if ( Input.GetButtonUp("Jump") ) {
            if (velocity.y > 0)
                velocity.y = velocity.y * .5f;
        }

        targetVelocity = move * maxSpeed;
    }
}
