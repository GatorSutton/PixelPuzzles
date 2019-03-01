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
    public GameObject alienPrefab;

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
            spawnRandomMoles(2);
        }
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(2f);
            spawnRandomMoles(3);
        }
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            spawnRandomMoles(4);
        }

        yield return new WaitForSeconds(5f);

        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(.1f);
            spawnRandomMoles(1);
        }

        yield return null;
    }

    void spawnRandomMoles(int count)
    {
        GameObject alien = Instantiate(alienPrefab, this.transform);
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
