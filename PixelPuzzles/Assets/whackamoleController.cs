using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whackamoleController : MonoBehaviour {

   //Randomly set flippable tiles
   //Flippable tiles self destruct in x amount of time
   //Stepping on the tile adds to score
   //HINT: DIVIDE AND CONQUER 

    Floor floor;
    [SerializeField]
    List<Tile> allTiles = new List<Tile>();


    // Use this for initialization
    void Start () {
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        StartCoroutine(playRound());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //add script in inspector to add strings for different get ready messages

    IEnumerator playRound()
    {
        //yield return getReady()
        yield return spawnMoles();
    }

    IEnumerator spawnMoles()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(3f);
            spawnRandomMole();
        }
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(2f);
            spawnRandomMole();
        }
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            spawnRandomMole();
        }

        yield return new WaitForSeconds(5f);

        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(.1f);
            spawnRandomMole();
        }

        yield return null;
    }

    void spawnRandomMole()
    {
        allTiles = floor.getAllTiles();
        for(int i = allTiles.Count-1; i >= 0; i--)          //remove all spots where there is a mole or player
        {
            if(allTiles[i].isPlayerHere() || allTiles[i].myState == Tile.States.MOLE)
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
        if(tile.myState == Tile.States.MOLE)
        {
            tile.myState = Tile.States.NONE;
        }
    }

}
