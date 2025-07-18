using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [Header("Tile Settings")]
    public Tilemap groundTilemap;       // Слой с землей
    public TileBase unfertilizedTile;   // Ссылка на неудобренный тайл
    public TileBase fertilizedTile;     // Ссылка на удобренный тайл

    private int _unfertilizedCount;

    void Start()
    {
        CountUnfertilizedTiles();
    }

    // Подсчет всех неудобренных плиток при старте
    private void CountUnfertilizedTiles()
    {
        _unfertilizedCount = 0;
        BoundsInt bounds = groundTilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = groundTilemap.GetTile(pos);
            if (tile != null && tile == unfertilizedTile)
            {
                _unfertilizedCount++;
            }
        }

        Debug.Log($"Начальное количество неудобренных плиток: {_unfertilizedCount}");
    }

    // Вызывается червяком при наступлении на плитку
    public void FertilizeTile(Vector3Int position)
    {
        TileBase currentTile = groundTilemap.GetTile(position);

        if (currentTile == unfertilizedTile)
        {
            // Заменяем тайл на удобренный
            groundTilemap.SetTile(position, fertilizedTile);

            _unfertilizedCount--;
            Debug.Log($"Осталось неудобренных: {_unfertilizedCount}");

            if (_unfertilizedCount <= 0)
            {
                Debug.Log("ПОБЕДА! Все плитки удобрены!");
            }
        }
    }
}
