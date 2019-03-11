using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateBall : MonoBehaviour {

    bool hasBall = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        hasBall = true;
    }

    private void OnTriggerExit(Collider other)
    {
        hasBall = false;
    }
}
