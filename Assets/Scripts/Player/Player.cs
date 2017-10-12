using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private static Player instance;
    private Rigidbody2D rb2d;

    // hard-coded in right now, can be changed later
    public float moveSpeed;
    public float climbSpeed;
    public float jumpForce;

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
        Physics2D.gravity = new Vector2(0, -10f);
        rb2d = GetComponent<Rigidbody2D>();

        moveSpeed = 5.5f;
        climbSpeed = 6f;
        jumpForce = 200f;
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
        rb2d.velocity = new Vector2(horizontal * moveSpeed, rb2d.velocity.y);

        // climbing
        if (onRope)
            rb2d.velocity = new Vector2(horizontal * climbSpeed, vertical * climbSpeed);
        if (onRope && Input.GetKey(KeyCode.Space)) {
            onRope = false;
        }

        // jump
        if (rb2d.velocity.y == 0 && Input.GetButton("Jump") && !onRope)
            rb2d.AddForce(new Vector2(0, jumpForce));
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Rope") {
            onRope = true;
            Physics2D.gravity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Rope") {
            onRope = false;
            Physics2D.gravity = new Vector2(0, -10f);
        }
    }
}
