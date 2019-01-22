using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDirection : MonoBehaviour {
    public float speed;
    public Vector3 Direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Direction * speed);
	}
}
