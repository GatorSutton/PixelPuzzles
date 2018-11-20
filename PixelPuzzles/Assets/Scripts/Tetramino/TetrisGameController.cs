using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TetrisGameController : MonoBehaviour {

    public ScanFloorForMatch genericTetranimo;
    public float gameTime;
    Floor floor;
    Tile[,] arrayOfTiles;


	// Use this for initialization
	void Start () {
        spawnTetranimo();
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        arrayOfTiles = floor.getArrayOfTiles();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.childCount < 1)
        {
            spawnTetranimo();
        }
        checkForClearedLines();

	}

    void spawnTetranimo()
    {
        Instantiate(genericTetranimo, transform);
        genericTetranimo.shape = (TetrisDefinitions.Shapes)Random.Range(0, System.Enum.GetValues(typeof(TetrisDefinitions.Shapes)).Length);
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
                deleteColumn(i);
            }

        }

    }

    void deleteColumn(int rowNumber)
    {
        for (int i = 0; i < arrayOfTiles.GetLength(1); i++)
        {
            arrayOfTiles[rowNumber, i].myState = Tile.States.NONE;
        }
    }


}
