using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float jumpForce;
    public float speed;
    public string playerNumber;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal" + playerNumber);
        float moveVertical = Input.GetAxis("Vertical" + playerNumber);

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement = Vector3.ClampMagnitude(movement, speed);

        transform.Translate(movement);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce);
        }

        if(rb.IsSleeping())
        {
            rb.WakeUp();
        }
    }


}
