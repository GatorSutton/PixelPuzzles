using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeController : MonoBehaviour
{

    

    Floor floor;
    List<Tile> playerTiles = new List<Tile>();
    List<Vector2> playerDistances = new List<Vector2>();
    float tiltFactor = 4;


    void Start()
    {
        floor = GameObject.Find("Floor").GetComponent<Floor>();
    }

    //Find all the tiles that have a player on them
    //Calculate a 2d vector based on the average of player tiles
    //Rotate the x and y axis of the maze based on the vector
    void Update()
    {
        playerDistances.Clear();
        playerTiles = floor.getAllTiles();
        for (int i = playerTiles.Count - 1; i >= 0; i--)
        {
            if (!playerTiles[i].isPlayerHere())
            {
                playerTiles.RemoveAt(i);
            }
        }

        foreach(Tile tile in playerTiles)
        {
            playerDistances.Add(calculateVector(tile));
        }

        Vector2 averageVector = new Vector2(0f,0f);
        foreach(Vector2 vector in playerDistances)
        {
            averageVector += vector;
        }
        if (playerDistances.Count > 0)
        {
            averageVector /= playerDistances.Count;
        }   

       // transform.eulerAngles = new Vector3(averageVector.y * tiltFactor, -averageVector.x * tiltFactor, 0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(averageVector.y * tiltFactor -90f, -averageVector.x * tiltFactor, 0f), .1f * tiltFactor);
    }

    Vector2 calculateVector(Tile tile)
    {
        float x = tile.transform.position.x;
        float z = tile.transform.position.z;
        return new Vector2(x, z);
    }

}

