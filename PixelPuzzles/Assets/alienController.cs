﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alienController : MonoBehaviour {

    //follows the path to earth
    //orbits earth if not destroyed while following first path
    //has a tile associated with itself
    //falls out of sight when destroyed

    Floor floor;
    [SerializeField]
    List<Tile> allTiles = new List<Tile>();


    // Use this for initialization
    void Start () {
        floor = GameObject.Find("Floor").GetComponent<Floor>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void spawnRandomMole()
    {
        allTiles = floor.getAllTiles();
        for (int i = allTiles.Count - 1; i >= 0; i--)          //remove all spots where there is a mole or player
        {
            if (allTiles[i].isPlayerHere() || allTiles[i].myState == Tile.States.MOLE)
            {
                allTiles.RemoveAt(i);
            }
        }

        int randomTile = Random.Range(0, allTiles.Count);
        allTiles[randomTile].myState = Tile.States.MOLE;
        StartCoroutine(moleReturnsToTheEarth(5f, allTiles[randomTile]));
    }

    //turns a mole back into a regular tile if not stepped on in time
    IEnumerator moleReturnsToTheEarth(float time, Tile tile)
    {
        yield return new WaitForSeconds(time);
        if (tile.myState == Tile.States.MOLE)
        {
            tile.myState = Tile.States.NONE;
        }
    }
}
