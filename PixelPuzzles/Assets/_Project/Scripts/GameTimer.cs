using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour {

    public float roundTime;

    float roundTimer;
    public float RoundTimer
    {
        get
        {
            return roundTimer;
        }
    }

    // Use this for initialization
    void Start () {
        roundTimer = roundTime;
    }
	
	// Update is called once per frame
	void Update () {
        roundTimer -= Time.deltaTime;

        if(roundTimer <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
