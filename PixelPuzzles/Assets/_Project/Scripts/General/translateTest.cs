using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class translateTest : MonoBehaviour {

    private bool dirRight = true;
    public float speed = 2.0f;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (dirRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= 10.0f)
        {
            dirRight = false;
        }

        if (transform.position.x <= -10)
        {
            dirRight = true;
        }
    }
}
