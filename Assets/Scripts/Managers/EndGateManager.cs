using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGateManager : MonoBehaviour {
    private GameManager gameManager;
    public bool unlocked = false;

    private void Start() {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update () {
		if (gameManager.chaos >= gameManager.maxChaos) {
            unlocked = true;
        }
	}

    private void OnTriggerEnter2D (Collider2D col) {
        if (col.transform.tag == "Player" && unlocked) {
            //Generate next level
            Debug.Log("Loading Next Level");
            gameManager.NextLevel();
        }
    }
}