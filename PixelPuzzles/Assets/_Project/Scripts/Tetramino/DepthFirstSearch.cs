using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DepthFirstSearch {

    static int ROW = 8, COL = 8;

    static bool isSafe(Tile[,] M, int row,
                    int col, bool[,] visited)
    {
        return (row >= 0) && (row < ROW) &&
               (col >= 0) && (col < COL) &&
               (M[row, col].isPlayerHere() &&
               (M[row, col].myState != Tile.States.SET) &&                                     //check for inbounds and player on tile and tile is not already set
               !visited[row, col]);
    }

    static void DFS(Tile[,] M, int row,
                    int col, bool[,] visited)
    {
        // These arrays are used to  
        // get row and column numbers 
        // of 4 neighbors of a given cell 
        int[] rowNbr = new int[] {-1, 0, 0, 1};
        int[] colNbr = new int[] {0, -1, 1, 0,};

        // Mark this cell 
        // as visited 
        visited[row, col] = true;

        // Recur for all  
        // connected neighbours 
        for (int k = 0; k < 4; ++k)
            if (isSafe(M, row + rowNbr[k], col +
                                colNbr[k], visited))
                DFS(M, row + rowNbr[k],
                       col + colNbr[k], visited);
    }

    static public int countIslands(Tile[,] M)
    {
        bool[,] visited = new bool[ROW, COL];


        // Initialize count as 0 and  
        // travese through the all  
        // cells of given matrix 
        int count = 0;
        for (int i = 0; i < ROW; ++i)
            for (int j = 0; j < COL; ++j)
                if (M[i, j].isPlayerHere() &&
                    !visited[i, j])
                {
                    // If a cell with value 1 is not 
                    // visited yet, then new island  
                    // found, Visit all cells in this 
                    // island and increment island count 
                    DFS(M, i, j, visited);
                    ++count;
                }

        return count;
    }

    static public bool[,] islandIsTetranimo(Tile[,] M, int row, int col)
    {
        bool[,] visited = new bool[ROW, COL];
        int count = 0;

        DFS(M, row, col, visited);


        for (int i = 0; i < visited.GetLength(0); i++)
        {
            for (int j = 0; j < visited.GetLength(1); j++)
            {
                if (visited[i, j] == true)
                {
                    count++;
                }
            }
        }

        if (count == 4)
        {
            return visited;
        }
        return null;


    }

    static public PointList getListOfNeighbors(bool[,] visited)
    {
        PointList neighborData = new PointList();
       

        int count = 0;

        for (int i = 0; i < visited.GetLength(0); i++)
        {
            for (int j = 0; j < visited.GetLength(1); j++)
            {
                if (visited[i, j] == true)
                {
                    neighborData.list.Add(new Point());
                    if (j + 1 < visited.GetLength(1))
                    {
                        if (visited[i, j + 1] == true)
                        {
                            neighborData.list[count].list.Add(TetrisDefinitions.Direction.Up);
                        }
                    }
                    if (j - 1 >= 0)
                    {
                        if (visited[i, j - 1] == true)
                        {
                            neighborData.list[count].list.Add(TetrisDefinitions.Direction.Down);
                        }
                    }
                    if (i + 1 < visited.GetLength(0))
                    {
                        if (visited[i + 1, j] == true)
                        {
                            neighborData.list[count].list.Add(TetrisDefinitions.Direction.Right);
                        }
                    }
                    if (i - 1 >= 0)
                    {
                        if (visited[i - 1, j] == true)
                        {
                            neighborData.list[count].list.Add(TetrisDefinitions.Direction.Left);
                        }
                    }
                    count++;
                    
                }
            }
        }

        return neighborData;
    }


}
