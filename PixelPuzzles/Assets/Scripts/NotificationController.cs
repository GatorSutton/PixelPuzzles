using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour {


    private Text notification;
    private float time;
    private Vector3 startPosition;

	// Use this for initialization
	void Start () {
        notification = GetComponent<Text>();
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if(time > 0){
            time -= Time.deltaTime;
        }

        notification.color = Color.Lerp(Color.clear, Color.white, time);
        transform.position = new Vector3(startPosition.x, Mathf.Lerp(startPosition.y+150f, startPosition.y, time), startPosition.z);

    }

    public void flashMessage(string message){
        notification.text = message;
        time = 2;
    }

}
