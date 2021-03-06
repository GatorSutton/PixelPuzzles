﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TetrisGameController : MonoBehaviour {


   
    public LivingParticleArrayController livingParticles;
    public ScanFloorForMatch genericTetranimo;
    public TetrisSpawner tetrisSpawner;
    public float gameTime;
    Floor floor;
    Tile[,] arrayOfTiles;

    public Transform leftSpawn;
    public Transform frontSpawn;
    public Transform rightSpawn;

    enum Direction { Left, Front, Right};
    public TetrisSpawner spawnerLeft, spawnerFront, spawnerRight;

    float roundTimer;
    TetrisExplosion TE;
    int lineCount;


	// Use this for initialization
	void Start () {
        TE = GetComponent<TetrisExplosion>();
        spawnerFront = Instantiate(tetrisSpawner, frontSpawn);
       // spawnerLeft = Instantiate(tetrisSpawner, leftSpawn);
       // spawnerRight = Instantiate(tetrisSpawner, rightSpawn);
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        arrayOfTiles = floor.getArrayOfTiles();

        setUpParticleFields();
    }
	
	// Update is called once per frame
	void Update () {
        checkForClearedLines();
        checkForWin();
	}

    void checkForClearedLines()
    {
        int numOfCompleteLines = 0;
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
                numOfCompleteLines++;
                TE.startExplode(frontSpawn, 2f);
                //  TE.startExplode(leftSpawn, 2f);
                // TE.startExplode(rightSpawn, 2f);
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
                numOfCompleteLines++;
                TE.startExplode(frontSpawn, 2f);
                //    TE.startExplode(leftSpawn, 2f);
                //   TE.startExplode(rightSpawn, 2f);
            }

        }

        if(numOfCompleteLines > 0)
        {
            ScoreController.AddScore(numOfCompleteLines * numOfCompleteLines * 5000);
            Destroy(this.gameObject, 5f);
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

    IEnumerator SpawnFrenzy()
    {

      //  Instantiate(genericTetranimo, this.transform);
        yield return null;
    }

    private void setUpParticleFields() 
    {
        LivingParticleArrayController lP = Instantiate(livingParticles, frontSpawn);
        lP = Instantiate(livingParticles, leftSpawn);
        lP = Instantiate(livingParticles, rightSpawn); 
    }

    private void OnDestroy()
    {
        floor.clearAllTiles();
    }

    void checkForWin()
    {
        if(lineCount > 1)
        {
            Destroy(this.gameObject);
        }
    }





}
