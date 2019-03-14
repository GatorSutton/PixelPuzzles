using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballLabyrinthController : MonoBehaviour {

    //A maze with a ball
    //Maze will rotate based on the position of the players on the board
    public Maze MazePrefab;
    public GameObject BallPrefab;
    public GameObject GoalPrefab;
    public Transform MazeSpawn;
    public Transform MazeDestination;
    public float dropInSpeed;

    GameObject ballInstance;
    Maze mazeInstance;

    mazeController mC;

	// Use this for initialization
	void Start () {
        //need a coroutine to build the maze, rotate, drop in, spawn ball
        StartCoroutine(spawnMaze(8, 8));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator spawnMaze(int x, int z)
    {
        mazeInstance = Instantiate(MazePrefab, MazeSpawn) as Maze;
        mazeInstance.size.x = x;
        mazeInstance.size.z = z;
        yield return StartCoroutine(mazeInstance.Generate());
        mazeInstance.transform.eulerAngles = new Vector3(-90f, 0f, 0f);
        while (Vector3.Distance(mazeInstance.transform.position, MazeDestination.transform.position) > .1f)
        {
            mazeInstance.transform.Translate(Vector3.back * Time.deltaTime * dropInSpeed);
            yield return null;
        }

        //add or activate controller for the maze
        mazeInstance.gameObject.AddComponent<mazeController>();

        //Spawn the ball and set goal point
        ballInstance = Instantiate(BallPrefab, mazeInstance.SpawnPoint);
        Instantiate(GoalPrefab, mazeInstance.GoalPoint);

       
    }

    public void respawnBall()
    {
        Destroy(ballInstance);
        ballInstance = Instantiate(BallPrefab, mazeInstance.SpawnPoint);
    }

    private void OnEnable()
    {
        MazeCellTrigger.OnSteppedOn += respawnBall;
    }

    private void OnDisable()
    {
        MazeCellTrigger.OnSteppedOn -= respawnBall;
    }
}
