using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    Camera mCamera;
    public string cameraName;

	// Use this for initialization
	void Start () {
        mCamera = GameObject.Find(cameraName).GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(mCamera.transform.position, Vector3.up);
	}
}
