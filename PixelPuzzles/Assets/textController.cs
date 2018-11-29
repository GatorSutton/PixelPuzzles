using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textController : MonoBehaviour {

    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setText(string inputString)
    {
        text.text = inputString;
        transform.position = new Vector3(0f, 0f, 0f);
    }

    public IEnumerator fadeOffScreen()
    {
        while(transform.position.y < 300)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
            yield return null;
        }
    }
}
