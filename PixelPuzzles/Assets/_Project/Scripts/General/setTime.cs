using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setTime : MonoBehaviour {

    Text text;
    float time;
    public GameTimer gt;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        time = gt.RoundTimer;
        text.text = Mathf.Ceil(time).ToString();
	}
}
