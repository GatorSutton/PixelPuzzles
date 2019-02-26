using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public float timeBetweenFlicker;
    // public enum States { NONE, WARN, FLICKEROFF, FIRE, DAMAGE, SWITCH, FAKEFIRE, SELECTOR, POTION};
    public enum States {NONE, SET, SHAPEANIMATION, FLIP, RED, ORANGE, YELLOW, GREEN, BLUE, PURPLE, WHITE, SELECTOR, NOTE, NOTEBAROFF, NOTEBARON, NOTEBARHIT,  MOLE};
    //[System.NonSerialized]
    public States myState = States.NONE;
    public Material[] materials;
    public MeshRenderer rend;

    bool lastFramePlayerHere = false;
    public bool playerHere = false;
    public States flippedState;
    public bool flipped = false;
    public bool hitFrame = false;

    [SerializeField]
    noteController note;

    // Use this for initialization
    void Start () {
        rend = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        updateMaterial();
        checkForPlayerOnFlip();
        checkForPlayerOnMole();
        checkForPlayerHit();
	}

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerHit();
        }
    }
    */


    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag == "Player")
        { 
            playerHere = true;
        }
    }

    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerHere = false;
        }

    }
    



    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "selector")
        {
            myState = States.SELECTOR;
        }

        if(other.tag == "note")
        {
            if (myState != States.NOTEBAROFF)
            {
                myState = States.NOTE;
            }
            else
            {
                myState = States.NOTEBARON;
                note = other.GetComponent<noteController>();

            }
        }
        if (other.tag == "notebar")
        {
            myState = States.NOTEBAROFF;
        }


    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "fire")
        {
            myState = States.NONE;
        }

        if (other.tag == "fakefire")
        {
            myState = States.NONE;
        }

        if (other.tag == "selector")
        {
            myState = States.NONE;
        }
        if (other.tag == "note")
        {
                if (myState != States.NOTEBARON)
                {
                    myState = States.NONE;
                }
                else
                {
                    myState = States.NOTEBAROFF;
                    note = null;
                }
        }
    }


    private void updateMaterial()
    {
        switch(myState)
        {
            case States.NONE:
                rend.material = materials[0];
                break;
            case States.SET:
                rend.material = materials[1];
                break;
            case States.SHAPEANIMATION:
                rend.material = materials[1];
                break;
            case States.FLIP:
                rend.material = materials[6];
                break;
            case States.RED:
                rend.material = materials[1];
                break;
            case States.ORANGE:
                rend.material = materials[2];
                break;
            case States.YELLOW:
                rend.material = materials[3];
                break;
            case States.GREEN:
                rend.material = materials[4];
                break;
            case States.BLUE:
                rend.material = materials[5];
                break;
            case States.PURPLE:
                rend.material = materials[6];
                break;
            case States.SELECTOR:
                rend.material = materials[4];
                break;
            case States.NOTE:
                rend.material = materials[5];
                break;
            case States.NOTEBAROFF:
                rend.material = materials[1];
                break;
            case States.NOTEBARON:
                rend.material = materials[6];
                break;
            case States.NOTEBARHIT:
                rend.material = materials[4];
                break;
            case States.MOLE:
                rend.material = materials[1];
                break;
                
                
        }
        
        if(playerHere && myState != States.SET && flipped != true && myState != States.SELECTOR && myState != States.NOTEBARHIT && myState != States.SHAPEANIMATION)
        {
            rend.material = materials[5];
        }

    }

    public bool isPlayerHere()
    {
        return playerHere;
    }


    private void checkForPlayerOnFlip()
    {
        if (playerHere && myState == States.FLIP)
        {
            myState = flippedState;
            flipped = true;
        }
    }

    private void checkForPlayerOnMole()
    {
        if (playerHere && myState == States.MOLE)
        {
            myState = States.NONE;
        }
    }

    private void playerHit()
    {
        
        if(note != null)
        {
            print("notehit");
            note.strikeNote();
            //coroutine that flashes the tile and then returns it to a notebar status
            StartCoroutine("NoteHitFeedback");

        }
    }

    private void checkForPlayerHit()
    {
        if (lastFramePlayerHere == false && playerHere == true)
        {
            playerHit();
        }
        lastFramePlayerHere = playerHere;
    }

    IEnumerator NoteHitFeedback()
    {
        myState = States.NOTEBARHIT;
        yield return new WaitForSeconds(.2f);
        myState = States.NOTEBAROFF;
    }

}
