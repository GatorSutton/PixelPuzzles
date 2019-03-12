using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setShieldColor : MonoBehaviour {

    public Material[] materials = new Material[3];
    MeshRenderer mr;

	// Use this for initialization
	void Awake () {
        mr = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setColor(Tile.States state)
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
