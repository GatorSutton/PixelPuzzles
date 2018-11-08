using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TetrisGameController : MonoBehaviour {

    public ScanFloorForMatch genericTetranimo;
    public float gameTime;


	// Use this for initialization
	void Start () {
        spawnTetranimo();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.childCount < 1)
        {
            spawnTetranimo();
        }
	}

    void spawnTetranimo()
    {
        Instantiate(genericTetranimo, transform);
        genericTetranimo.shape = (TetrisDefinitions.Shapes)Random.Range(0, System.Enum.GetValues(typeof(TetrisDefinitions.Shapes)).Length);
    }


}
