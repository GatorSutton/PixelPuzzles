using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColors : MonoBehaviour {

    public enum Color { None, Red, Orange, Yellow, Green, Blue, Purple, White };

    public static Color TileStateToColor(Tile.States state)
    {
        switch (state)
        {
            case Tile.States.NONE:
                return Color.None;

            case Tile.States.SET:
                return Color.Red;

            case Tile.States.SHAPEANIMATION:
                return Color.Red;

            case Tile.States.FLIP:
                return Color.Purple;

            case Tile.States.RED:
                return Color.Red;

            case Tile.States.ORANGE:
                return Color.Orange;

            case Tile.States.YELLOW:
                return Color.Yellow;

            case Tile.States.GREEN:
                return Color.Green;

            case Tile.States.BLUE:
                return Color.Blue;

            case Tile.States.PURPLE:
                return Color.Purple;

            case Tile.States.WHITE:
                return Color.White;

            case Tile.States.SELECTOR:
                return Color.Green;

            case Tile.States.NOTE:
                return Color.Blue;

            case Tile.States.NOTEBAROFF:
                return Color.Red;

            case Tile.States.NOTEBARON:
                return Color.Purple;

            case Tile.States.MOLE:
                return Color.Red;

            default:
                break;
        }

        return Color.None;
    }

    public static char ColorToChar(Color color)
    {
        switch (color)
        {
            case Color.None:
                return '0';
            case Color.Red:
                return '1';
            case Color.Orange:
                return '2';
            case Color.Yellow:
                return '3';
            case Color.Green:
                return '4';
            case Color.Blue:
                return '5';
            case Color.Purple:
                return '6';
            case Color.White:
                return '7';
            default:
                break;
        }

        return '0';
    }
}
