﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private static Player instance;
    private Rigidbody2D rb2d;

    public float moveSpeed;
    public float climbSpeed;
    public float jumpSpeed;

    public bool onRope;

    private IUseable useable;

    public static Player Instance {
        get {
            if (instance == null)
                instance = GameObject.FindObjectOfType<Player>();
            return instance;
        }
    }

    private void Start() {
        onRope = false;

        rb2d = GetComponent<Rigidbody2D>();

        moveSpeed = 5.5f;
        climbSpeed = 6f;
        jumpSpeed = 5f;

        Player.Instance.rb2d.gravityScale = 1f;
    }

    void FixedUpdate() {
        Movement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.E))
            Use();
    }

    void Use() {
        if (useable != null)
            useable.Use();
    }


    void Movement(float horizontal, float vertical) {

        //Note: this is not what we want to use, accessing rigidbody.velocity directly can cause unpredictable, and hard to control
        //results, use Rigidbody2D.MovePosition instead
        
        //Using MovePosition also means that we don't have to use the gravity component on the rigidbody, and can control the player's
        //gravity in this script

		rb2d.velocity = new Vector2(horizontal * moveSpeed, rb2d.velocity.y);

        // climbing
        if (onRope)
            rb2d.velocity = new Vector2(horizontal * climbSpeed, vertical * climbSpeed);

        if (onRope && Input.GetKey(KeyCode.Space)) {
            onRope = false;
            Player.Instance.rb2d.gravityScale = 1f;
        }

        // jump

        //Note: when using rigidBody.AddForce, it is important to use the ForceMode VelocityChange,
        //meaning that a velocity needs to be calculated to reach a certain height (mgh = 1/2mv^2; v^2 = 2gh; v = sqrt(2h|g|)

        //I think testing the vertical component of the velocity is a clever way to check if grounded, but what if we want to 
        //create slanted platforms that the player can slide and jump off of?

        if (rb2d.velocity.y == 0 && Input.GetKey(KeyCode.Space) && !onRope)
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
    }


    // current bug where player is able to pass through platform if he is on a rope even if there is no rope below it


    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Rope") {
            onRope = true;
            gameObject.layer = 12;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Rope") {
            onRope = false;
            gameObject.layer = 8;
        }
    }
}
