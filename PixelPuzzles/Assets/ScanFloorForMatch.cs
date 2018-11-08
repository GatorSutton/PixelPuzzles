using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanFloorForMatch : MonoBehaviour {

    Floor floor;
    Tile[,] tileArray;
    bool checkingShapeInProgress = false;

    public List<enumShape> shapeMap;

    [SerializeField]
    public float percentComplete = 0;

    private PointList neighborData = new PointList();

    public TetrisDefinitions.Shapes shape;

	// Use this for initialization
	void Start () {
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        tileArray = floor.getArrayOfTiles();
        neighborData.rotate();

        Instantiate(shapeMap.Find(x => x.shape == shape).shapePrefab, this.transform);
    }
	
	// Update is called once per frame
	void Update () {
        //loop through all the squares
        
        for (int i = 0; i < tileArray.GetLength(0); i++)
        {
            for (int j = 0; j < tileArray.GetLength(1); j++)
            {
                if (tileArray[i, j].isPlayerHere())
                {
                    bool[,] visited = DepthFirstSearch.islandIsTetranimo(tileArray, i, j);
                        if(visited != null)
                    {
                        neighborData = DepthFirstSearch.getListOfNeighbors(visited);
                        if(TetrisDefinitions.CheckForShapeMatch(shape, neighborData) && !checkingShapeInProgress)
                        {
                            checkingShapeInProgress = true;
                            StartCoroutine(constantCheckShape(5f, i, j, visited));                                                      //check that shape holds for set amount of time
                        }
                    }
                }
            }
        }

	}

    IEnumerator constantCheckShape(float time, int i, int j, bool[,] visited)
    {
        bool shapeHolding = true;
        float timer = 0;

        while(timer < time && shapeHolding)
        {
            Debug.Log("checking" + timer);
            timer += Time.deltaTime;
            percentComplete = timer / time;
            shapeHolding = CheckShape(i, j, shape);
            yield return null;
        }
        if(timer >= time)
        {
            for (int x = 0; x < visited.GetLength(0); x++)
            {
                for (int y = 0; y < visited.GetLength(1); y++)
                {
                    if(visited[x,y] == true)
                    {
                        tileArray[x, y].myState = Tile.States.FAKEFIRE;
                    }
                }
            }
            Destroy(this.gameObject);
        }

        percentComplete = 0;
        checkingShapeInProgress = false;
        yield return null;
    }

    private bool CheckShape(int i, int j, TetrisDefinitions.Shapes shape)
    {
        bool[,] visited = DepthFirstSearch.islandIsTetranimo(tileArray, i, j);
        if(visited != null)
        {
            neighborData = DepthFirstSearch.getListOfNeighbors(visited);
            if(TetrisDefinitions.CheckForShapeMatch(shape, neighborData))
            {
                return true;
            }
        }

        return false;
        
    }

}
