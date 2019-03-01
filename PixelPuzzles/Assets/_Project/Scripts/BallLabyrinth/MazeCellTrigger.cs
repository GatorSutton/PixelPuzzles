using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCellTrigger : MonoBehaviour {

    public bool hasBall = false;
    MazeCell mc;
    Floor floor;
    Tile thisTile;
    ballLabyrinthController bLC;

    // Use this for initialization
    void Start () {
        mc = GetComponent<MazeCell>();
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        thisTile = floor.getTile(mc.coordinates.x, mc.coordinates.z);
        bLC = GameObject.Find("LabyrinthGameController").GetComponent<ballLabyrinthController>();
	}

    private void Update()
    {
        if(hasBall && thisTile.playerHere)
        {
            //respawn the ball
            thisTile.myState = Tile.States.NONE;
            bLC.respawnBall();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        hasBall = true;
        thisTile.myState = Tile.States.GREEN;
    }

    private void OnTriggerExit(Collider other)
    {
        hasBall = false;
        thisTile.myState = Tile.States.NONE;
    }
}
