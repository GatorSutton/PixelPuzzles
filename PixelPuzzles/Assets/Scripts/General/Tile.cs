using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public float timeBetweenFlicker;
    // public enum States { NONE, WARN, FLICKEROFF, FIRE, DAMAGE, SWITCH, FAKEFIRE, SELECTOR, POTION};
    public enum States {NONE, SET, FLIP, RED, ORANGE, YELLOW, GREEN, BLUE, PURPLE, SELECTOR};
    //[System.NonSerialized]
    public States myState = States.NONE;
    public Material[] materials;
    public MeshRenderer rend;

    public bool playerHere = false;
    public States flippedState;
    public bool flipped = false;

    // Use this for initialization
    void Start () {
        rend = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        updateMaterial();
        checkForPlayerOnFlip();
	}
    

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
                
        }
        

        if(playerHere && myState != States.SET && flipped != true && myState != States.SELECTOR)
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

}
