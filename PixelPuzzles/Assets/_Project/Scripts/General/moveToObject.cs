using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToObject : MonoBehaviour {

    public float speed = 2.0f;
    Transform target;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("GameCenter").transform;
	}
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
	}
}
