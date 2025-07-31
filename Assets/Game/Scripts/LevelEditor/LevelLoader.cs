using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }

    [Header("Apple Prefab Reference")]
    [SerializeField] private GameObject _applePrefab;

    private Tilemap _groundTileMap;
    private Tilemap _boundaryTileMap;

    private TileBase _unfertileTile;
    private TileBase _fertileTile;
    private TileBase _boundaryTile;

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
        _groundTileMap = TilesManager.Instance.GroundTileMap;
        _boundaryTileMap = TilesManager.Instance.BoundaryTileMap;

        _unfertileTile = TilesManager.Instance.UnfertileTile;
        _fertileTile = TilesManager.Instance.FertileTile;
        _boundaryTile = TilesManager.Instance.BoundaryTile;
    }

    public void LoadLevel(LevelData data, int levelNumber)
    {
        ClearLevel();

        LoadGroundTiles(data);

        LoadBoundaryTiles(data);

        SpawnApples(data);

        SpawnWorm(data);
    }

    private void LoadGroundTiles(LevelData data)
    {
        int countUnfertileTile = 0;
        foreach (GroundTileData tileData in data.groundTiles)
        {
            Vector3Int cell = new Vector3Int(tileData.position.x, tileData.position.y, 0);
            TileBase tileToSet;
            if (tileData.type == GroundTileType.Fertilized)
            {
                tileToSet = _fertileTile;
            }
            else
            {
                tileToSet = _unfertileTile;
                countUnfertileTile++;
            }
            _groundTileMap.SetTile(cell, tileToSet);
        }
        GameManager.Instance.InitializeLevel(data.levelNumber, countUnfertileTile);
    }

    private void LoadBoundaryTiles(LevelData data)
    {
        foreach (BoundaryTileData tileData in data.boundaryTiles)
        {
            Vector3Int cell = new Vector3Int(tileData.position.x, tileData.position.y, 0);
            _boundaryTileMap.SetTile(cell, _boundaryTile);
        }
    }

    private void SpawnApples(LevelData data)
    {
        foreach (Vector2Int pos in data.applePositions)
        {
            Vector3 worldPos = _groundTileMap.GetCellCenterWorld(new Vector3Int(pos.x, pos.y, 0));
            Instantiate(_applePrefab, worldPos, Quaternion.identity);
        }
    }
    private void SpawnWorm(LevelData data) 
    {
        Vector3 headWorldPos = _groundTileMap.GetCellCenterWorld(
            new Vector3Int(data.wormStartPos.x, data.wormStartPos.y, 0));

        WormController.Instance.PreparingToGame(headWorldPos, data.wormStartSegments);
    }

    private void ClearLevel()
    {
        _groundTileMap.ClearAllTiles();
        _boundaryTileMap.ClearAllTiles();

        // ”дал€ем €блоки
        GameObject[] apples = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject apple in apples) Destroy(apple);

        // ”дал€ем сегменты тела
        GameObject[] wormBody = GameObject.FindGameObjectsWithTag("Body");
        foreach (GameObject body in wormBody) Destroy(body);
    }
}