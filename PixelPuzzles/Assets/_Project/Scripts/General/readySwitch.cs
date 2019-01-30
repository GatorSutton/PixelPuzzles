using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class readySwitch : MonoBehaviour {

    BoxCollider boxCollider;
    [SerializeField]
    List<Tile> selectorTiles = new List<Tile>();
    [SerializeField]
    List<Tile> notSelectorTiles = new List<Tile>();
    Floor floor;
    public float percentage;

    void Awake()
    {
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        notSelectorTiles = floor.getAllTiles();
    }

    // Use this for initialization
    void Start () {
        boxCollider = GetComponent<BoxCollider>();
        percentage = 0;
    }
	
	// Update is called once per frame
	void Update () {
            checkForOnSelector();
            checkForSelected();
    }

    private void checkForOnSelector()
    {
        bool onButton = false;
        bool offButton = false;

        foreach (Tile tile in selectorTiles)
        {
            if (tile.isPlayerHere())
            {
                onButton = true;
            }
        }
        foreach (Tile tile in notSelectorTiles)
        {
            if (tile.isPlayerHere())
            {
                offButton = true;
            }
        }

        if (onButton && !offButton && percentage < 1)
        {
            percentage += Time.deltaTime * .3f;
        }
        else if (percentage > 0 && percentage < 1)
        {
            percentage -= Time.deltaTime * .5f;
        }
    }

    private void checkForSelected()
    {
        if (percentage >= 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "tile")
        {
            selectorTiles.Add(other.GetComponent<Tile>());
            notSelectorTiles.Remove(other.GetComponent<Tile>());
        }
    }

    private void OnDestroy()
    {
        floor.clearAllTiles();
    }
}
