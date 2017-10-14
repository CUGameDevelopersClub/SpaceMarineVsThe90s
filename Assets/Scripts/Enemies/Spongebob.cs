using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spongebob : Enemy {

	// Use this for initialization
	void Start () {
        EnemySetUp(10, EnemyType.Walking, 10, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		GroundMovement (transform.position + new Vector3 (1, -1.5f), transform.position + new Vector3 (-1, -1.5f), true); // true is a debug thing
		
	}
}
