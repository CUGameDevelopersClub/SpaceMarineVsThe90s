using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private static Player instance;

    // hard-coded in right now, can be changed later
    public float moveSpeed = 5.5f;
    public float climbSpeed = 6f;

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
    }

    void Update() {
        Movement();
        if (Input.GetKey(KeyCode.E))
            Use();
    }

    void Use() {
        if (useable != null)
            useable.Use();
    }

    void Movement() {
        // left movement
        if (Input.GetKey(KeyCode.A)) 
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // right movement
        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        // climbing
        if (onRope && Input.GetKey(KeyCode.W)) {
            transform.position += Vector3.up * climbSpeed * Time.deltaTime;
            Physics2D.gravity = Vector2.zero;
        } else if ( onRope && Input.GetKey(KeyCode.S)) {
            transform.position += Vector3.down * climbSpeed * Time.deltaTime;
            Physics2D.gravity = Vector2.zero;
        } else if (Input.GetKey(KeyCode.Space)) {
            onRope = false;
        }

        if (!onRope)
            Physics2D.gravity = new Vector2(0, -10f);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Rope") {
            onRope = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Rope") {
            onRope = false;
        }
    }
}
