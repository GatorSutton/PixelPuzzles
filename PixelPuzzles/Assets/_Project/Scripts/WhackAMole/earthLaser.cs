using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthLaser : MonoBehaviour {

    Floor floor;
    [SerializeField]
    List<Tile> allTiles = new List<Tile>();

    alienController currentAlien;

    Tile greenTile;
    Tile blueTile;
    Tile redTile;

    void Start () {
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        allTiles = floor.getAllTiles();

        greenTile = spawnLaser(Tile.States.GREEN);
        blueTile = spawnLaser(Tile.States.BLUE);
        redTile = spawnLaser(Tile.States.RED);

    }
	
	void Update () {
        checkForFire();
	}

    
    Tile spawnLaser(Tile.States state)
    {
    allTiles = floor.getAllTiles();
        for (int i = allTiles.Count - 1; i >= 0; i--)          //remove all spots where there is a mole or player
        {
            if (allTiles[i].isPlayerHere() ||
                allTiles[i].myState == Tile.States.GREEN ||
                allTiles[i].myState == Tile.States.BLUE ||
                allTiles[i].myState == Tile.States.RED)
        {
            allTiles.RemoveAt(i);
        }

    }

    int randomTile = Random.Range(0, allTiles.Count);
    allTiles[randomTile].myState = state;
    return allTiles[randomTile];
    }

    void checkForFire()
    {
        if(greenTile.isPlayerHere())
        {
            fireLaser(greenTile.myState);
            greenTile.myState = Tile.States.NONE;
            greenTile = spawnLaser(Tile.States.GREEN);
        }

        if (blueTile.isPlayerHere())
        {
            fireLaser(blueTile.myState);
            blueTile.myState = Tile.States.NONE;
            blueTile = spawnLaser(Tile.States.BLUE);
        }

        if (redTile.isPlayerHere())
        {
            fireLaser(redTile.myState);
            redTile.myState = Tile.States.NONE;
            redTile = spawnLaser(Tile.States.RED);
        }
    }

    //hit the closest alien with a laser
    void fireLaser(Tile.States state)
    {
        if (currentAlien != null)
        {
            currentAlien.TakeHit(state);
        }

    }

    public void setCurrentAlien(alienController aC)
    {
        currentAlien = aC;

    }

}
