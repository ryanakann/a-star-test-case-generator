using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileGenerator : MonoBehaviour
{
    public static TileGenerator instance;

    public GameObject tilePrefab;
    public GameObject tilesRoot;
    public TMP_InputField textOutput;
    public TMP_InputField widthField;
    public TMP_InputField heightField;
    public TMP_InputField scaleField;
    public List<Tile> tiles;

    public Tile.Type selectedTileType = Tile.Type.Path;
    public Tile startTile;
    public Tile finishTile;

    public int width = 16;
    public int height = 16;
    public float tileScale = 1f;

    public bool turnedOn;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        turnedOn = true;
        tilesRoot = new GameObject("Tiles Parent");
    }

    

    // Start is called before the first frame update
    void Generate()
    {
        print("Generate");

        tiles = new List<Tile>();
        startTile = null;
        finishTile = null;


        float xOffset = width * tileScale / 2f;
        float yOffset = height * tileScale / 2f;
        int index = 0;
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                GameObject instance = Instantiate(tilePrefab, new Vector3(i * tileScale - xOffset, -j * tileScale + yOffset), Quaternion.identity);
                Tile tile = instance.GetComponent<Tile>();
                tiles.Add(tile);
                instance.transform.localScale = Vector3.one * tileScale;
                instance.GetComponent<Tile>().Init(index++);
                instance.transform.SetParent(tilesRoot.transform);

                for (int l = -1; l <= 1; l++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        if (k == 0 && l == 0) continue;

                        int neighborIndex = (i + k) * width + j + k;
                        //float weight = (k != 0 && l != 0 ? Mathf.Sqrt(2f) : 1f);
                        float weight = 1f;
                        instance.GetComponent<Tile>().AddNeighbor(neighborIndex, weight);
                    }
                }
            }
        }
    }

    public void TurnOn()
    {
        turnedOn = true;
    }

    public void TurnOff()
    {
        turnedOn = false;
    }

    public void ConvertMapToText()
    {
        string output = "";
        int index = 0;
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                Tile tile = tiles[index++];
                switch (tile.type)
                {
                    case Tile.Type.Path:
                        output += "O";
                        break;
                    case Tile.Type.Wall:
                        output += "X";
                        break;
                    case Tile.Type.Start:
                        output += "S";
                        break;
                    case Tile.Type.Finish:
                        output += "F";
                        break;
                    default:
                        break;
                }

                if (i < width - 1) output += " ";
            }
            output += "\n";
        }
        textOutput.text = output;
    }

    public void ResetTiles()
    {
        print("Here");
        int.TryParse(widthField.text, out width);
        int.TryParse(heightField.text, out height);
        float.TryParse(scaleField.text, out tileScale);
        foreach (Transform child in tilesRoot.transform)
        {
            Destroy(child.gameObject);
        }
        tiles.Clear();
        Generate();
    }
}
