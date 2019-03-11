using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeWalker : MonoBehaviour
{

    //move GameObject between all of the nodes of a nodePathSystem

    public nodePathSystem nPS;
    public int speed;
    public bool reachedEnd = false;

    private float progress;
    [SerializeField]
    private Vector3 currentNode;

    // Use this for initialization
    void Start()
    {
        nPS = GameObject.Find("NodePath").GetComponent<nodePathSystem>();
        transform.position = nPS.StartPosition();
        currentNode = nPS.nodePath[1];

    }

    //Move the transfrom between current 2 nodes
    //Move up the current two nodes if destination reached and another node exists
    //Stop moving at the last node
    void Update()
    {
       

        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentNode, step);

        
        if (Vector3.Distance(transform.position, currentNode) < 0.001f)
        {
            if (nPS.currentIndex(currentNode) < nPS.nodePath.Count - 1)
            {
                currentNode = nPS.NextNode(currentNode);
            }
            else
            {
                reachedEnd = true;
            }
        }
        
    }
}
