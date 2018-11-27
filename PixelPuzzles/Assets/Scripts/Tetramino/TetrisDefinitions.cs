using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TetrisDefinitions {

    public enum Shapes {None, J, L, S, Square, Straight, T, Z};

    public enum Direction { Up, Down, Right, Left };

    public static Direction RotateClockWise (Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                dir = Direction.Right;
                break;
            case Direction.Down:
                dir = Direction.Left;
                break;
            case Direction.Right:
                dir = Direction.Down;
                break;
            case Direction.Left:
                dir = Direction.Up;
                break;
            default:
                break;
        }
        return dir;
    }

    public static bool CheckForShapeMatch(Shapes shape, PointList neighborData)
    {
        bool matches = false;
        PointList pointList = possibleMatchesOfShapes(shape);


        for (int i = 0; i < 4; i++)
        {
            pointList.rotate();
            if(PointList.samePointList(pointList, neighborData))
            {
                return matches = true;
            }
        }




        return matches;
    }

    static PointList possibleMatchesOfShapes(Shapes shape)
    {

        PointList possibleMatches = new PointList();

        switch (shape)
        {
            case Shapes.J:
                    possibleMatches.list.Add(new Point(false, true, false, false));
                    possibleMatches.list.Add(new Point(true, false, true, false));
                    possibleMatches.list.Add(new Point(false, false, true, true));
                    possibleMatches.list.Add(new Point(false, false, false, true));
                break;
            case Shapes.L:
                    possibleMatches.list.Add(new Point(false, true, false, false));
                    possibleMatches.list.Add(new Point(true, false, false, true));
                    possibleMatches.list.Add(new Point(false, false, true, true));
                    possibleMatches.list.Add(new Point(false, false, true, false));
                break;
            case Shapes.S:
                    possibleMatches.list.Add(new Point(false, false, true, false));
                    possibleMatches.list.Add(new Point(true, false, false, true));
                    possibleMatches.list.Add(new Point(false, true, true, false));
                    possibleMatches.list.Add(new Point(false, false, false, true));
                break;
            case Shapes.Square:
                    possibleMatches.list.Add(new Point(true, false, true, false));
                    possibleMatches.list.Add(new Point(true, false, false, true));
                    possibleMatches.list.Add(new Point(false, true, true, false));
                    possibleMatches.list.Add(new Point(false, true, false, true));
                break;
            case Shapes.Straight:
                    possibleMatches.list.Add(new Point(true, false, false, false));
                    possibleMatches.list.Add(new Point(true, true, false, false));
                    possibleMatches.list.Add(new Point(true, true, false, false));
                    possibleMatches.list.Add(new Point(false, true, false, false));         
                break;
            case Shapes.T:
                    possibleMatches.list.Add(new Point(true, false, true, true));
                    possibleMatches.list.Add(new Point(false, true, false, false));
                    possibleMatches.list.Add(new Point(false, false, true, false));
                    possibleMatches.list.Add(new Point(false, false, false, true));
                break;
            case Shapes.Z:
                    possibleMatches.list.Add(new Point(false, false, true, false));
                    possibleMatches.list.Add(new Point(false, true, false, true));
                    possibleMatches.list.Add(new Point(true, false, true, false));
                    possibleMatches.list.Add(new Point(false, false, false, true));
                break;
            default:
                break;
        }

        return possibleMatches;
    }



    
}

[System.Serializable]
public class Point
{

    public Point()
    {
        list = new List<TetrisDefinitions.Direction>();
    }

    public List<TetrisDefinitions.Direction> list;

    
    public Point(bool up, bool down, bool right, bool left)
    {
        list = new List<TetrisDefinitions.Direction>();

        if (up)
        {
            list.Add(TetrisDefinitions.Direction.Up);
        }
        if (down)
        {
            list.Add(TetrisDefinitions.Direction.Down);
        }
        if (right)
        {
            list.Add(TetrisDefinitions.Direction.Right);
        }
        if (left)
        {
            list.Add(TetrisDefinitions.Direction.Left);
        }
    }

    // Rotate all of the neighbor directions inside of a single Point
    public void rotate()
    {
        for (int i = 0; i < this.list.Count; i++)
        {
            list[i] = TetrisDefinitions.RotateClockWise(list[i]);
        }
    }


}

[System.Serializable]
public class PointList 
{

    public PointList()
    {
        list = new List<Point>();
    }

    public List<Point> list;

    public static bool samePointList(PointList A, PointList B)
    {
        PointList Astorage = new PointList();
        PointList Bstorage = new PointList();


        int count = 0;
        int listSize = A.list.Count;

        foreach(var a in A.list.ToArray())
        {
            foreach(var b in B.list.ToArray())
            {
               // if(a.list.SequenceEqual(b.list))
               if(!b.list.Except(a.list).Any() && b.list.Count == a.list.Count)
                {
                    count++;
                    A.list.Remove(a);
                    Astorage.list.Add(a);
                    B.list.Remove(b);
                    Bstorage.list.Add(b);
                }
            }
        }

        A.list.AddRange(Astorage.list);
        B.list.AddRange(Bstorage.list);

        return (count == listSize);
    }

    // Rotate all of the Points in a PointList
    public void rotate()
    {
        for (int i = 0; i < this.list.Count; i++)
        {
            list[i].rotate();
        }
    }

}
