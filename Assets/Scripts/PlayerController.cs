using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectMovement {

    public float jumpSpeed = 15; // per second
    public float maxSpeed = 15; // per second

    // Rope climbing
    public bool onRope = false;

	// Use this for initialization
	void Start ()
    {
        transform.position = spawnPoint.position;
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

    void climb()
    {
        Vector2 move = Vector2.zero;
        velocity.y = jumpSpeed;
        targetVelocity = move * maxSpeed;

        //float y = Input.GetAxis("Vertical");
        //Vector2 movement = new Vector3(0.0f, y, 0.0f);
        //rb2d.velocity = movement.normalized * maxSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Rope")
        {
            onRope = true;

            // Climbs rope if w key is being pressed, we can change this later
            if (onRope && Input.GetKeyDown("W"))
            {
                climb();
            }
        }
        else
        {
            onRope = false;
        }

    }
}
