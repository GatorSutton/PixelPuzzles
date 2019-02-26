using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanFloorForMatch : MonoBehaviour {

    Floor floor;
    Tile[,] tileArray;
    bool checkingShapeInProgress = false;
    public progressBar pB;

    public float timeToMakeShape;
    public List<enumShape> shapeMap;

    public float percentComplete = 0;

    private PointList neighborData = new PointList();

    public TetrisDefinitions.Shapes shape;
    private shapeController visualShape;
    private moveTetrominoToCenter mTTC;

    [SerializeField]
    List<Tile> currentTiles = new List<Tile>();
    int currentAnimatedTile = 0;
    int previousAnimatedTile = 3;
    float timer;

	// Use this for initialization
	void Start () {
        mTTC = GetComponent<moveTetrominoToCenter>();
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        tileArray = floor.getArrayOfTiles();
        neighborData.rotate();

        visualShape = Instantiate(shapeMap.Find(x => x.shape == shape).shapePrefab, this.transform).GetComponent<shapeController>();
    }
	
	// Update is called once per frame
	void Update () {

        updateAnimatedTiles();
   

        if (mTTC !=null && mTTC.centered)
        {
            //loop through all the squares
            for (int i = 0; i < tileArray.GetLength(0); i++)
            {
                for (int j = 0; j < tileArray.GetLength(1); j++)
                {
                    if (tileArray[i, j].isPlayerHere() && tileArray[i, j].myState != Tile.States.SET)
                    {
                        bool[,] visited = DepthFirstSearch.islandIsTetranimo(tileArray, i, j);

                        if (visited != null)
                        {
                            neighborData = DepthFirstSearch.getListOfNeighbors(visited);
                            if (TetrisDefinitions.CheckForShapeMatch(shape, neighborData) && !checkingShapeInProgress)
                            {
                                checkingShapeInProgress = true;
                                StartCoroutine(constantCheckShape(timeToMakeShape, i, j, visited));                                                      //check that shape holds for set amount of time

                                for (int k = 0; k < tileArray.GetLength(0); k++)
                                {
                                    for (int l = 0; l < tileArray.GetLength(1); l++)
                                    {
                                        if (visited[k, l] == true)
                                        {
                                            currentTiles.Add(tileArray[k, l]);
                                        }
                                    }
                                }                           //add the current shape of tiles to a list

                            }
                        }
                    }
                }
            }
        }
        pB.setPercent(percentComplete);

    }

    IEnumerator constantCheckShape(float time, int i, int j, bool[,] visited)
    {
        bool shapeHolding = true;
        float timer = 0;

        while(timer < time && shapeHolding)
        {
            timer += Time.deltaTime;
            percentComplete = timer / time;
            shapeHolding = CheckShape(i, j, shape);
            yield return null;
        }
        resetCurrentTiles();
        if (timer >= time)
        {
            for (int x = 0; x < visited.GetLength(0); x++)
            {
                for (int y = 0; y < visited.GetLength(1); y++)
                {
                    if(visited[x,y] == true)
                    {
                        tileArray[x, y].myState = Tile.States.SET;
                    }
                }
            }
            visualShape.selfDestruct();
            ScoreController.AddScore(100);
            Destroy(this.gameObject);
            //tetrominoReset();
        }

        percentComplete = 0;
        checkingShapeInProgress = false;
        yield return null;
    }

    private bool CheckShape(int i, int j, TetrisDefinitions.Shapes shape)
    {
        if(!tileArray[i, j].isPlayerHere())
        {
            return false;
        }
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

    private void tetrominoReset()
    {
        visualShape = Instantiate(shapeMap.Find(x => x.shape == shape).shapePrefab, this.transform).GetComponent<shapeController>();
        transform.localPosition = new Vector3(0f, -5f, 0f);
    }


    void updateAnimatedTiles()
    {
        if (currentTiles.Count != 0)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                timer = Mathf.Lerp(.3f, .03f, percentComplete);
                currentTiles[currentAnimatedTile].myState = Tile.States.SHAPEANIMATION;
                currentTiles[previousAnimatedTile].myState = Tile.States.NONE;
                previousAnimatedTile = currentAnimatedTile;
                if(currentAnimatedTile == 3)
                {
                    currentAnimatedTile = 0;
                }
                else
                {
                    currentAnimatedTile++;
                }
                
            }
        }
    }

    void resetCurrentTiles()
    {
        for (int i = 0; i < currentTiles.Count; i++)
        {
            currentTiles[i].myState = Tile.States.NONE;
        }
        currentTiles.Clear();
    }

}
