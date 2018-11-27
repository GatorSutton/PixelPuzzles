using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TetrisGameController : MonoBehaviour {

    
    public LivingParticleArrayController livingParticles;
    public ScanFloorForMatch genericTetranimo;
    public TetrisSpawner tetrisSpawner;
    public float gameTime;
    Floor floor;
    Tile[,] arrayOfTiles;
    grabBagRandom gBR;

    public Transform leftSpawn;
    public Transform frontSpawn;
    public Transform rightSpawn;

    enum Direction { Left, Front, Right};
    public TetrisSpawner spawnerLeft, spawnerFront, spawnerRight;

	// Use this for initialization
	void Start () {
        gBR = GetComponent<grabBagRandom>();
        //  spawnTetronimo(Direction.Front);
        spawnerFront = Instantiate(tetrisSpawner, frontSpawn);
        spawnerLeft = Instantiate(tetrisSpawner, leftSpawn);
        spawnerRight = Instantiate(tetrisSpawner, rightSpawn);
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        arrayOfTiles = floor.getArrayOfTiles();

        setUpParticleFields();
    }
	
	// Update is called once per frame
	void Update () {

        checkForClearedLines();
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

    private void setUpParticleFields()
    {
        LivingParticleArrayController lP = Instantiate(livingParticles, frontSpawn);
        lP.transform.localPosition = new Vector3(0f, 0f, 1f);
        lP = Instantiate(livingParticles, leftSpawn);
        lP.transform.localPosition = new Vector3(0f, 0f, 1f);
        lP.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        lP = Instantiate(livingParticles, rightSpawn);
        lP.transform.localPosition = new Vector3(0f, 0f, 1f);
        lP.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }

    

}
