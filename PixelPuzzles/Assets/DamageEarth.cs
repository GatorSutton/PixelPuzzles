using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEarth : MonoBehaviour {

    NodeWalker NW;

	// Use this for initialization
	void Start () {
        NW = GetComponent<NodeWalker>();
	}
	
	// Update is called once per frame
	void Update () {
		if(NW.reachedEnd)
        {
            GameObject Earth = GameObject.Find("Earth");
            Destroy(Earth);
        }
	}
}
