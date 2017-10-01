using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private GameObject player;
    private Vector3 offset; // offset distance between player and camera

	// Use this for initialization
	void Start () {
		offset = this.GetComponent<Transform>().position;
		player = GameObject.FindGameObjectWithTag("Player");
	}

    private void LateUpdate()
    {
        // prevents camera from following player rotation
		transform.position = player.transform.position + offset;
    }
}
