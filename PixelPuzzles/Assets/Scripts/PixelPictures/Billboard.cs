using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    Camera camera;
    public string cameraName;

	// Use this for initialization
	void Start () {
        camera = GameObject.Find(cameraName).GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(camera.transform.position, Vector3.up);
	}
}
