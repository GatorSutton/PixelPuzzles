using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TetrisGameController : MonoBehaviour {

    public ScanFloorForMatch genericTetranimo;
    public float gameTime;
    Floor floor;
    Tile[,] arrayOfTiles;
    GameObject tetromino;
    bool waitingForSpawn = false;

	// Use this for initialization
	void Start () {
        spawnTetronimo();
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        arrayOfTiles = floor.getArrayOfTiles();
    }
	
	// Update is called once per frame
	void Update () {

        if(tetromino == null && !waitingForSpawn)
        {
            waitingForSpawn = true;
            Invoke("spawnTetronimo", 3);
        }

        checkForClearedLines();

	}

    GameObject spawnTetronimo()
    {
        waitingForSpawn = false;
        tetromino = Instantiate(genericTetranimo, transform).gameObject;
        genericTetranimo.shape = (TetrisDefinitions.Shapes)Random.Range(0, System.Enum.GetValues(typeof(TetrisDefinitions.Shapes)).Length);
        return tetromino;
    }

    void checkForClearedLines()
    {
        //Columns
        for (int i = 0; i < arrayOfTiles.GetLength(0); i++)
        {
            bool completeLine = true;
            for (int j = 0; j < arrayOfTiles.GetLength(1); j++)
            {
                if(arrayOfTiles[i,j].myState != Tile.States.SET)
                {
                    completeLine = false;
                }
            }
            if(completeLine)
            {
                StartCoroutine(deleteColumn(i));
            }

        }


        //Rows
        for (int i = 0; i < arrayOfTiles.GetLength(1); i++)
        {
            bool completeLine = true;
            for (int j = 0; j < arrayOfTiles.GetLength(0); j++)
            {
                if (arrayOfTiles[j, i].myState != Tile.States.SET)
                {
                    completeLine = false;
                }
            }
            if (completeLine)
            {
                StartCoroutine(deleteRow(i));
            }

        }

    }

    IEnumerator deleteColumn(int columnNumber)
    {
        for (int i = 0; i < arrayOfTiles.GetLength(1); i++)
        {
            arrayOfTiles[columnNumber, i].myState = Tile.States.NONE;
            yield return new WaitForSeconds(.2f);
        }
    }

    IEnumerator deleteRow(int rowNumber)
    {
        for (int i = 0; i < arrayOfTiles.GetLength(1); i++)
        {
            arrayOfTiles[i, rowNumber].myState = Tile.States.NONE;
            yield return new WaitForSeconds(.2f);
        }
    }


}
