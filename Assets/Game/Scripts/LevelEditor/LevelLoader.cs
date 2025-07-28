using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelLoader : MonoBehaviour
{
    [Header("Level to load")]
    [SerializeField] private LevelData levelData;

    [Header("TileMap Reference")]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap boundaryTilemap;

    [Header("Prefabs Reference")]
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject wormHeadPrefab;

    [Header("Ground TileBase")]
    [SerializeField] private TileBase unfertilizedTile;
    [SerializeField] private TileBase fertilizedTile;

    [Header("Boundary TileBase")]
    [SerializeField] private TileBase boundaryTile;

    private void Start()
    {
        LoadLevel(levelData);
    }

    public void LoadLevel(LevelData data)
    {
        ClearLevel();

        LoadGroundTiles(data);
        LoadBoundaryTiles(data);

        SpawnApples(data);

        SpawnWorm(data);

        GameManager.Instance.InitializeGrid();
    }

    private void LoadGroundTiles(LevelData data)
    {
        foreach (GroundTileData tileData in data.groundTiles)
        {
            Vector3Int cell = new Vector3Int(tileData.position.x, tileData.position.y, 0);
            TileBase tileToSet = tileData.type == GroundTileType.Fertilized ? fertilizedTile : unfertilizedTile;
            groundTilemap.SetTile(cell, tileToSet);
        }
    }

    private void LoadBoundaryTiles(LevelData data)
    {
        foreach (BoundaryTileData tileData in data.boundaryTiles)
        {
            Vector3Int cell = new Vector3Int(tileData.position.x, tileData.position.y, 0);
            boundaryTilemap.SetTile(cell, boundaryTile);
        }
    }

    private void SpawnApples(LevelData data)
    {
        foreach (Vector2Int pos in data.applePositions)
        {
            Vector3 worldPos = groundTilemap.GetCellCenterWorld(new Vector3Int(pos.x, pos.y, 0));
            Instantiate(applePrefab, worldPos, Quaternion.identity);
        }
    }
    private void SpawnWorm(LevelData data)
    {
        Vector3 headWorldPos = groundTilemap.GetCellCenterWorld(
            new Vector3Int(data.wormStartPos.x, data.wormStartPos.y, 0));

        GameObject wormHead = Instantiate(wormHeadPrefab, headWorldPos, Quaternion.identity);
        WormController worm = wormHead.GetComponent<WormController>();

        if (worm != null)
        {
            worm.Initialize(groundTilemap, boundaryTilemap);
            worm.CreateSegmentsOnStart(data.wormStartSegments);
        }
    }

    private void ClearLevel()
    {
        groundTilemap.ClearAllTiles();
        boundaryTilemap.ClearAllTiles();

        // ”дал€ем €блоки
        GameObject[] apples = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject apple in apples) Destroy(apple);

        // ”дал€ем голову
        GameObject head = GameObject.FindGameObjectWithTag("Player");
        if (head) Destroy(head);
    }
}
