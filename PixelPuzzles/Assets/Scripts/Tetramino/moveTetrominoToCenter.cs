using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTetrominoToCenter : MonoBehaviour {

    public bool centered = false;
    Vector3 home = new Vector3(0f, 0f, 0f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, home, Time.deltaTime);
        if(Vector3.Distance(transform.localPosition, home) < .01f)
        {
            centered = true;
        }
    }
}
