using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballGoal : MonoBehaviour {

    GameObject ballGameController;

	// Use this for initialization
	void Start () {
        ballGameController = transform.parent.parent.parent.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ball")
        {
            Debug.Log("WIN");
            ScoreController.AddScore(5000);
            Destroy(ballGameController);
        }
    }
}
