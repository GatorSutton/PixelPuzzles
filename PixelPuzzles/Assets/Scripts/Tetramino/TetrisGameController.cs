using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TetrisGameController : MonoBehaviour {

    public LivingParticleArrayController livingParticles;
    public ScanFloorForMatch genericTetranimo;
    public float gameTime;
    Floor floor;
    Tile[,] arrayOfTiles;
    GameObject tetromino;
    bool waitingForSpawn = false;

   // public Transform leftSpawn;
    public Transform frontSpawn;
   // public Transform rightSpawn;

	// Use this for initialization
	void Start () {
        spawnTetronimo();
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        arrayOfTiles = floor.getArrayOfTiles();

        LivingParticleArrayController lP = Instantiate(livingParticles, frontSpawn);
        lP.transform.localPosition = new Vector3(0f, 0f, 1f);
    }
	
	// Update is called once per frame
	void Update () {

        if(tetromino == null && !waitingForSpawn)
        {
            waitingForSpawn = true;
            Invoke("spawnTetronimo", 2);
        }
        checkForClearedLines();

	}

    GameObject spawnTetronimo()
    {
        waitingForSpawn = false;
        tetromino = Instantiate(genericTetranimo, frontSpawn.transform).gameObject;
        tetromino.transform.localPosition = new Vector3(0f, -3f, 0f);
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
              //  StartCoroutine(explosion());
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
              //  StartCoroutine(explosion());
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

    /*
    IEnumerator explosion()
    {
        float timer = 0;

        while (timer < 2)
        {
            GameObject shape = Instantiate(genericTetranimo, frontSpawn.transform).gameObject;
            Destroy(shape.GetComponent<moveTetrominoToCenter>());
            Destroy(shape.GetComponentInChildren<affectorAdder>());
            shape.AddComponent<deathTimer>();
            timer += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
    */

}
