using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballSteppedOn : MonoBehaviour {

    Maze maze;
    public List<MazeCell> allCells;

	// Use this for initialization
	void Start () {
        maze = GetComponent<Maze>();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
