using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tile : MonoBehaviour
{
    public enum Type { Path, Wall, Start, Finish }
    public Type type = Type.Path;
    public int index;
    public List<(int, float)> neighbors;
    public void Init(int index)
    {
        this.index = index;
        name = $"Tile({index})";
        neighbors = new List<(int, float)>();
    }

    public void AddNeighbor(int index, float weight)
    {
        neighbors.Add((index, weight));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetType(Type type)
    {
        this.type = type;
        switch (type)
        {
            case Type.Path:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case Type.Wall:
                GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            case Type.Start:
                GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case Type.Finish:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            default:
                break;
        }
    }

    private void OnMouseOver()
    {
        if (!TileGenerator.instance.turnedOn) return;
        if (Input.GetMouseButton(0))
        {
            if (TileGenerator.instance.selectedTileType == Type.Start)
            {
                TileGenerator.instance.startTile?.SetType(Type.Path);
                TileGenerator.instance.startTile = this;
            }
            else if (TileGenerator.instance.selectedTileType == Type.Finish)
            {
                TileGenerator.instance.finishTile?.SetType(Type.Path);
                TileGenerator.instance.finishTile = this;
            }
            SetType(TileGenerator.instance.selectedTileType);
        }
    }
}
