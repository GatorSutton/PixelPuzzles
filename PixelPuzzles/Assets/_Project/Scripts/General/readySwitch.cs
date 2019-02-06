using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class readySwitch : MonoBehaviour {

    BoxCollider boxCollider;
    [SerializeField]
    List<Tile> selectorTiles = new List<Tile>();
    [SerializeField]
    List<Tile> notSelectorTiles = new List<Tile>();
    Floor floor;
    float time;
    public float timeToSync;
    public Slider slider;

    void Awake()
    {
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        notSelectorTiles = floor.getAllTiles();
    }

    // Use this for initialization
    void Start () {
        boxCollider = GetComponent<BoxCollider>();
        time = 0;
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

        if (onButton && !offButton && time < timeToSync)
        {
            time += Time.deltaTime * .3f;
        }
        else if (time > 0 && time < timeToSync)
        {
            time -= Time.deltaTime * .5f;
        }

        slider.value = time / timeToSync;
    }

    private void checkForSelected()
    {
        if (time >= timeToSync)
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
