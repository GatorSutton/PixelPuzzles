using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class noteController : MonoBehaviour {

    public delegate void NoteStruck();
    public static event NoteStruck OnNoteStrike;

    public bool struck = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void strikeNote()
    {
        struck = true;
        if(OnNoteStrike != null)
        {
            OnNoteStrike();
        }

        Destroy(this.gameObject);
    }
}
