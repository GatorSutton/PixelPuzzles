using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballLabyrinthController : MonoBehaviour {

    //A maze with a ball
    //Maze will rotate based on the position of the players on the board
    public Maze mazePrefab;
    public GameObject ball;
    public Transform MazeSpawn;

    mazeController mC;

	// Use this for initialization
	void Start () {
        //mC = Instantiate(mazePrefab, MazeSpawn).GetComponent<mazeController>();
        var mazeInstance = Instantiate(mazePrefab, MazeSpawn) as Maze;
        StartCoroutine(mazeInstance.Generate());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
