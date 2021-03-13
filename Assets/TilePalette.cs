using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePalette : MonoBehaviour
{

    public Tile.Type paletteType;

    // Start is called before the first frame update
    void Start()
    {
        switch (paletteType)
        {
            case Tile.Type.Path:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case Tile.Type.Wall:
                GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            case Tile.Type.Start:
                GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case Tile.Type.Finish:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            default:
                break;
        }
    }

    private void OnMouseDown()
    {
        if (!TileGenerator.instance.turnedOn) return;
        TileGenerator.instance.selectedTileType = paletteType;
    }
}
