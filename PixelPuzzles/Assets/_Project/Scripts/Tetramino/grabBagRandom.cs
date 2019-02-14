using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabBagRandom : MonoBehaviour {

    public List<TetrisDefinitions.Shapes> possibleShapes = new List<TetrisDefinitions.Shapes>();
    [SerializeField]
    List<TetrisDefinitions.Shapes> bagOfShapes = new List<TetrisDefinitions.Shapes>();
    TetrisGameController tGC;

    // Use this for initialization
    void Start () {
        tGC = GetComponent<TetrisGameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public TetrisDefinitions.Shapes getRandomFromList()
    {
        if(bagOfShapes.Count <= 0)
        {
            resetGrabBag();
        }

        int randomInt = Random.Range(0, bagOfShapes.Count);
        TetrisDefinitions.Shapes shape = bagOfShapes[randomInt];
        bagOfShapes.Remove(shape);
        return shape;
    }

    void resetGrabBag()
    {

        foreach(TetrisDefinitions.Shapes shape in possibleShapes)
        {
            bagOfShapes.Add(shape);
        }
        if (tGC.spawnerLeft != null && tGC.spawnerLeft.currentShape != TetrisDefinitions.Shapes.None)
        {
            bagOfShapes.Remove(tGC.spawnerRight.currentShape);
            bagOfShapes.Remove(tGC.spawnerFront.currentShape);
            bagOfShapes.Remove(tGC.spawnerLeft.currentShape);
        }
    }


    void removeActiveShapes()
    {

    }
}
