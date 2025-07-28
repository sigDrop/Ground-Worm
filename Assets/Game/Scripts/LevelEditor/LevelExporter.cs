using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelExporter : MonoBehaviour
{
    [Header("Tilemaps")]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap boundaryTilemap;

    [Header("Ground TileBase")]
    [SerializeField] private TileBase fertilizedTile;

    [Header("Special Objects")]
    [SerializeField] private WormStartPoint wormStart;
    [SerializeField] private Transform applesParent;

    [Header("Export Settings")]
    [SerializeField] private string saveFolder = "Assets/Game/ScriptableObjects/LevelsData/";

    [ContextMenu("Export Level")]
    public void ExportLevel()
    {
        if (wormStart == null)
        {
            Debug.LogError("Не указан WormStartPoint!");
            return;
        }

        LevelData levelData = ScriptableObject.CreateInstance<LevelData>();
        levelData.levelName = "Level_" + GetUniqueIndex();
        levelData.levelNumber = GetUniqueIndex();

        // Сохраняем землю
        foreach (Vector3Int cell in groundTilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = groundTilemap.GetTile(cell);

            if (tile != null)
            {
                Vector2Int pos = new Vector2Int(cell.x, cell.y);
                GroundTileType type = tile == fertilizedTile ? GroundTileType.Fertilized : GroundTileType.Unfertilized;
                levelData.groundTiles.Add(new GroundTileData(pos, type));
            }    
        }

        // Сохраняем границы
        foreach (Vector3Int cell in boundaryTilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = boundaryTilemap.GetTile(cell);

            if (tile != null)
            {
                Vector2Int pos = new Vector2Int(cell.x, cell.y);
                levelData.boundaryTiles.Add(new BoundaryTileData(pos));
            }
        }

        // Сохраняем старт червяка
        levelData.wormStartPos = GetSnappedCell(wormStart.transform.position);
        levelData.wormStartSegments = wormStart.startBodySegments;

        // Сохраняем яблоки
        levelData.applePositions.Clear();
        foreach (Transform apple in applesParent)
        {
            Vector2Int cell = GetSnappedCell(apple.position);
            levelData.applePositions.Add(cell);
        }

        // Сохраняем
        string path = saveFolder + levelData.levelName + ".asset";
        AssetDatabase.CreateAsset(levelData, path);
        AssetDatabase.SaveAssets();

        Debug.Log($"Уровень экспортирован: {path}");
    }

    private Vector2Int GetSnappedCell(Vector3 worldPosition)
    {
        Vector3Int cell = groundTilemap.WorldToCell(worldPosition);
        return new Vector2Int(cell.x, cell.y);
    }

    private int GetUniqueIndex()
    {
        int index = 0;
        while (System.IO.File.Exists(saveFolder + $"Level_{index}.asset"))
        {
            index++;
        }
        return index;
    }
}