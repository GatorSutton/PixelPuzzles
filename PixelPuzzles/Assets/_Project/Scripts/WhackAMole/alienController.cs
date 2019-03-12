using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class alienController : MonoBehaviour {

    //follows the path to earth
    //orbits earth if not destroyed while following first path
    //has a tile associated with itself
    //falls out of sight when destroyed

    Floor floor;
    List<Tile> allTiles = new List<Tile>();
    Tile tile;
    public Material[] materials = new Material[3];
    public setShieldColor SC;

    [SerializeField]
    List<Tile.States> listOfShields = new List<Tile.States>();
    int currentShield = 2;
    public bool isAlive = true;

    public MeshRenderer mr;


    // Use this for initialization
    void Start () {
        // floor = GameObject.Find("Floor").GetComponent<Floor>();
        //  spawnRandomMole();
        listOfShields.Add(Tile.States.BLUE);
        listOfShields.Add(Tile.States.GREEN);
        listOfShields.Add(Tile.States.RED);

        listOfShields = listOfShields.OrderBy(x => Random.value).ToList();

        setColor(listOfShields[0]);
        SC.setColor(listOfShields[2]);
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void TakeHit(Tile.States state)
    {
        if(state == listOfShields[currentShield])
        {
            if (currentShield == 0)
            {
                isAlive = false;
                Destroy(this.gameObject);
            }
            else if(currentShield == 2)
            {
                currentShield--;
                SC.setColor(listOfShields[currentShield]);
            }
            else
            {
                currentShield--;
                Destroy(SC.gameObject);

            }

        }
    }

    void setColor(Tile.States state)
    {
        switch (state)
        {
            case Tile.States.GREEN:
                mr.material = materials[0];
                break;
            case Tile.States.BLUE:
                mr.material = materials[1];
                break;
            case Tile.States.RED:
                mr.material = materials[2];
                break;
            default:
                break;
        }
    }

}
