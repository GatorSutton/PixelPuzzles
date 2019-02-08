using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Floor : MonoBehaviour {

    public Tile tilePrefab;
    public int sizeX;
    public int sizeZ;

    public int modularHeight;
    public int modularWidth;
    public int localHeight;
    public int localWidth;


    [System.NonSerialized]
    private Tile[,] tiles;
    ArduinoCommunicator AC;
    ArduinoCommunicator AC2;
    ArduinoBitCommunicator ACSensors;

	// Use this for initialization
	void Awake () {
        CreateFloor();
    }

    private void Start()
    {
        AC = GameObject.Find("ArduinoByteSender").GetComponent<ArduinoCommunicator>();
       // AC2 = GameObject.Find("ArduinoCommunicator2").GetComponent<ArduinoCommunicator>();
        ACSensors = GameObject.Find("ArduinoBitListener").GetComponent<ArduinoBitCommunicator>();
    }

    public List<Tile> getAllTiles()
    {
        List<Tile> allTiles = new List<Tile>();
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                allTiles.Add(tiles[i, j]);
            }
        }
        return allTiles;
    }

    public Tile[,] getArrayOfTiles()
    {
        return tiles;
    }

    private void Update()
    {
        if (ACSensors != null)
        {
            checkForRealPlayer();
        }
        if (AC != null)
        {
            setFloorData();
        }
    }


    private void CreateTile(int x, int z)
    {
        Tile newTile = Instantiate(tilePrefab);
        tiles[x, z] = newTile;
        newTile.name = "Tile " + x + ", " + z;
        newTile.transform.localPosition = new Vector3(x - sizeX * 0.5f + 0.5f, 0f, z - sizeZ * 0.5f + 0.5f);
        newTile.transform.parent = transform;
    }

    private void CreateFloor()
    {
        tiles = new Tile[sizeX, sizeZ];
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                CreateTile(x, z);
            }
        }
    }

    public Tile getTile(int x, int z)
    {
        return tiles[x, z];
    }

    public void setTile(int x, int z, Tile.States state)
    {
        tiles[x, z].myState = state;
    }

    public bool isPlayerOnAnyTile()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                if(tiles[x, z].isPlayerHere())
                {
                    return true;
                }
            }
        }
        return false;
    }

    /*
    public void setSwitchTiles()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                tiles[x, z].myState = Tile.States.SWITCH;
            }
        }
    }

    public bool isEverySwitchOff()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                if (tiles[x, z].myState == Tile.States.SWITCH)
                {
                    return false;
                }
            }
        }
        return true;
    }
    */
    public void clearAllTiles()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                tiles[x, z].myState = Tile.States.NONE;
            }
        }
    }

    private void checkForRealPlayer()
    {

        BitArray list = ACSensors.getMessageIN();

        /*
        if (list.Length == sizeX * sizeZ)
        {
            for (int x = 0; x < sizeX; x++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    tiles[x, z].playerHere = list[(x * sizeZ) + z];
                }
            }
        }
        */
            int count = 0;
            for (int width = 0; width < modularWidth; width++)
            {
                for (int height = 0; height < modularHeight; height++)
                {
                    for (int x = 0; x < localWidth; x++)
                    {
                        for (int z = 0; z < localHeight; z++)
                        {
                            tiles[x + (width * localWidth), z + (height * localHeight)].playerHere = list[count++];
                        }
                    }
                }
            }
    }

    private void setFloorData()
    {
        byte[] list = new byte[sizeX * sizeZ]; 
        byte value;


        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                // value = (byte)(tiles[x, z].myState + 48);
                value = (byte)TileColors.ColorToChar(TileColors.TileStateToColor(tiles[x, z].myState));
                if (tiles[x, z].myState == Tile.States.NONE && tiles[x, z].isPlayerHere())
                {
                    //value = 70;
                    value = (byte)TileColors.ColorToChar(TileColors.Color.Blue);
                }
                list[(x * sizeZ) + z] = value;
            }
        }


        string output = Encoding.UTF8.GetString(list, 0, sizeX*sizeZ);
        AC.setMessageOUT(output);
       // AC2.setMessageOUT(output);
    }
     
}
