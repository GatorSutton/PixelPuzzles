using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tetranimoText : MonoBehaviour {

    [SerializeField]
    private float percentComplete;
    private ScanFloorForMatch sFFM;

    public Text text;



	// Use this for initialization
	void Start () {
        sFFM = gameObject.GetComponentInParent<ScanFloorForMatch>();
        text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        percentComplete = sFFM.percentComplete;
        text.text = Mathf.Round((percentComplete*100)) + "%";
	}
}
