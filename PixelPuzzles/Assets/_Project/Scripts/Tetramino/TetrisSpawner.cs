using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisSpawner : MonoBehaviour {

    public ScanFloorForMatch genericTetromino;
    grabBagRandom gBR;

    GameObject tetromino;
    bool waitingForSpawn = false;
    public TetrisDefinitions.Shapes currentShape = TetrisDefinitions.Shapes.None;

	// Use this for initialization
	void Start () {
     //   gBR = GameObject.Find("TetrisGameController").GetComponent<grabBagRandom>();
        gBR = transform.parent.parent.parent.GetComponent<grabBagRandom>();
        spawnTetronimo();
	}
	
	// Update is called once per frame
	void Update () {
        if (tetromino == null && !waitingForSpawn)
        {
            waitingForSpawn = true;
            Invoke("spawnTetronimo", 2);
        }
    }


    void spawnTetronimo()
    {
        waitingForSpawn = false;
        genericTetromino.shape = gBR.getRandomFromList();
        currentShape = genericTetromino.shape;
        tetromino = Instantiate(genericTetromino, transform).gameObject;
        tetromino.transform.localPosition = new Vector3(0f, -3f, 0f);
    }
}
