using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  

public class notificationController : MonoBehaviour {

    int hits = 0;
    Text text;
    int notesInARow = 0;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void addToHitsCount()
    {
        hits++;
        text.text = "Hits: " + hits.ToString();
        ScoreController.AddScore(100);
    }

    void OnEnable()
    {
        noteController.OnNoteStrike += addToHitsCount;
    }


    void OnDisable()
    {
        noteController.OnNoteStrike -= addToHitsCount;
    }
}
