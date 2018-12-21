using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Selector : MonoBehaviour {

    BoxCollider boxCollider;
    [SerializeField]
    List<Tile> selectorTiles = new List<Tile>();
    [SerializeField]
    List<Tile> notSelectorTiles = new List<Tile>();
    Floor floor;
    public bool on = true;
    public progressBar pB;
    
    public ImageDefinitions.Direction direction;
    public float percentage;


    // Use this for initialization
    void Start () {
        boxCollider = GetComponent<BoxCollider>();
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        percentage = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (on)
        {
            checkForOnSelector();
            checkForSelected();
        }
        if(pB!=null)
        pB.setPercent(percentage);
	}

    public void toggleBoxCollider()
    {
        boxCollider.enabled = !boxCollider.enabled;
    }

    public void startInitializeSelector()
    {
        StartCoroutine(initializeSelector());
    }

    private IEnumerator initializeSelector()
    {
        toggleBoxCollider();
        yield return new WaitForFixedUpdate();
        toggleBoxCollider();
        while(selectorTiles.Count == 0)
        {
            yield return null;
        }
        notSelectorTiles = floor.getAllTiles().Except(selectorTiles).ToList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "tile")
        {
            selectorTiles.Add(other.GetComponent<Tile>());
        }
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
            if(tile.isPlayerHere())
            {
                offButton = true;
            }
        }

        if(onButton && !offButton && percentage < 1)
        {
            percentage += Time.deltaTime * .3f;
        }
        else if(percentage > 0 && percentage < 1)
        {
            percentage -= Time.deltaTime * .5f;
        }
    }

    private void checkForSelected()
    {
        if(percentage >= 1)
        {
            gameObject.SendMessageUpwards("AnswerSelected", direction);
        }
    }

}
