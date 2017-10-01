using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform player;
    public Vector3 offset; // offset distance between player and camera

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
    }

    private void LateUpdate()
    {
        // prevents camera from following player rotation
        float newXPos = player.transform.position.x - offset.x;
        transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);
    }
}
