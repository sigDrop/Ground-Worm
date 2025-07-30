using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Tilemap groundTilemap;
    public TileBase unfertileTile;
    public TileBase fertileTile;

    private int _countUnfertileTile;

    private int _currentLevel;

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

    public void InitializeGrid(int levelNumber)
    {
        _countUnfertileTile = 0;
        _currentLevel = levelNumber;

        BoundsInt bounds = groundTilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = groundTilemap.GetTile(pos);
            if (tile == unfertileTile)
            {
                _countUnfertileTile++;
            }
        }

        TimerManager.Instance.ResetTimer();

        TimerManager.Instance.StartTimer();
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
        TimerManager.Instance.StopTimer();
        UIManager.Instance.ShowVictoryPanel(_currentLevel, TimerManager.Instance.GetTime());
    }
}
