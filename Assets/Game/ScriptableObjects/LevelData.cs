using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level_", menuName = "Game/ScriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName = "New Level";

    public int levelNumber;

    // Плитки
    public List<GroundTileData> groundTiles = new List<GroundTileData>();
    public List<BoundaryTileData> boundaryTiles = new List<BoundaryTileData>();

    // Стартовая позиция червяка
    public Vector2Int wormStartPos;
    public int wormStartSegments;

    // Яблоки
    public List<Vector2Int> applePositions = new List<Vector2Int>();
}

public enum GroundTileType//Тип плиток земли
{
    Unfertilized, // Неудобренные
    Fertilized // Удобренные
}

[System.Serializable]
public struct GroundTileData
{
    public Vector2Int position;
    public GroundTileType type;

    public GroundTileData(Vector2Int pos, GroundTileType tileType)
    {
        position = pos;
        type = tileType;
    }
}

[System.Serializable]
public struct BoundaryTileData
{
    public Vector2Int position;

    public BoundaryTileData(Vector2Int pos)
    {
        position = pos;
    }
}