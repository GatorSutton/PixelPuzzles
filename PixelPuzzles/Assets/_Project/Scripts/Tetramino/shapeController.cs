using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shapeController : MonoBehaviour {

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void selfDestruct()
    {
        transform.parent = null;
        rb.AddRelativeForce(0f, 0f, 100f);
        Destroy(gameObject, 10f);
    }
}
