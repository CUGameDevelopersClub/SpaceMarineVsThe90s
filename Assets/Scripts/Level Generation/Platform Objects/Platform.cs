using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    private static Platform instance;

    public static Platform Instance {
        get {
            if (instance == null)
                instance = GameObject.FindObjectOfType<Platform>();
            return instance;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

}
