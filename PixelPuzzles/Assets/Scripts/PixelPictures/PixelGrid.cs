using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelGrid : MonoBehaviour {

    Texture2D pixelMap;
    [SerializeField]

    Tile.States convertPixelToState(Color32 pixel)
    {
         var a = (int)pixel.a;
         var r = (int)pixel.r;
         var g = (int)pixel.g;
         var b = (int)pixel.b;

        Tile.States state;

        if(a == 0)
        {
            return Tile.States.NONE;
        }

        switch (r)
        {
            case 255:
                if(g == 0)
                {
                    state = Tile.States.RED;
                }
                else if(g <= 127)
                {
                    state = Tile.States.ORANGE;
                }
                else
                {
                    state = Tile.States.YELLOW;
                }
                break;
            case 0:
                if(g == 255)
                {
                    state = Tile.States.GREEN;
                }
                else
                {
                    state = Tile.States.BLUE;
                }
                break;
            default:
                    state = Tile.States.PURPLE;
                break;
        }

        return state;

    }

    public List<Tile.States> createStatesMap()
    {
        List<Tile.States> statesMap = new List<Tile.States>();
        for (int i = 0; i < pixelMap.height; i++)
        {
            for (int j = 0; j < pixelMap.width; j++)
            {
                var pixel = pixelMap.GetPixel(i, j);
                statesMap.Add(convertPixelToState(pixel));
            }
        }

        return statesMap;
    }

    public void setPixelMap()
    {
        pixelMap = GetComponent<SpriteRenderer>().sprite.texture;
    }
}
