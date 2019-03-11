using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whackamoleController : MonoBehaviour {


    Floor floor;

    List<Tile> allTiles = new List<Tile>();
    public alienController alienPrefab;
    [SerializeField]
    List<alienController> allAliens = new List<alienController>();
    int alienCount = 0;
    public earthLaser eL;


    // Use this for initialization
    void Start () {
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        StartCoroutine(playRound());
    }
	
	// Update is called once per frame
	void Update () {
        
        if(alienCount == 0 && allAliens.Count == 1)
        {
            eL.setCurrentAlien(allAliens[0]);
        }

        if(allAliens.Count > 0 && !allAliens[0].isAlive)
        {
            allAliens.RemoveAt(0);
            if (allAliens.Count != 0) { eL.setCurrentAlien(allAliens[0]); }
        }

        alienCount = allAliens.Count;
	}

    //add script in inspector to add strings for different get ready messages

    IEnumerator playRound()
    {
        //yield return getReady()
        yield return spawnMoles();
    }

    IEnumerator spawnMoles()
    {
        for (int i = 0; i < 1; i++)
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
        /*
        
        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(.1f);
            spawnRandomMoles(1);
        }
        */

        yield return null;
    }

    void spawnRandomMoles(int count)
    {
        alienController alien = Instantiate(alienPrefab, this.transform);
        allAliens.Add(alien);
    }

}
