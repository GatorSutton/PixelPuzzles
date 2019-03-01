using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class nodePathSystem: MonoBehaviour {


    public List<Vector3> nodePath = new List<Vector3>();

	// Use this for initialization
	void Awake () {
        foreach (Transform child in transform)
        {
            nodePath.Add(child.transform.position);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 StartPosition()
    {
        return nodePath[0];
    }

    public Vector3 NextNode(Vector3 currentNode)
    {
        int index = nodePath.IndexOf(nodePath.Find(x => x == currentNode));
        return nodePath[++index];
    }

    public int currentIndex(Vector3 currentNode)
    {
        return nodePath.IndexOf(nodePath.Find(x => x == currentNode));
    }
}
