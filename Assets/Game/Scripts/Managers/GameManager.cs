using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Tilemap groundTilemap;
    public TileBase unfertileTile;
    public TileBase fertileTile;

    private int _countUnfertileTile;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        BoundsInt bounds = groundTilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = groundTilemap.GetTile(pos);
            if (tile == unfertileTile)
            {
                _countUnfertileTile++;
            }
        }
    }

    public void NewFertileTile()
    {
        _countUnfertileTile--;
        if (_countUnfertileTile <= 0)
        {
            AllTilesFertile();
        }
    }

    private void AllTilesFertile()
    {
        Debug.Log("Level Complete!");
    }
}
