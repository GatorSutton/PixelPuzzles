using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceTarget : MonoBehaviour {

    private Transform target;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("Floor").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(2 * transform.position - target.position);
    }
}
