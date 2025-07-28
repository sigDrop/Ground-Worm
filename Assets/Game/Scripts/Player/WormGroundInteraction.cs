using UnityEngine;
using UnityEngine.Tilemaps;

public class WormGroundInteraction : MonoBehaviour
{
    public static WormGroundInteraction Instance;

    [Header("TileMaps References")]
    [SerializeField] private Tilemap _groundTileMap;

    [Header("TileAssets References")]
    [SerializeField] private TileBase _unfertileTile;
    [SerializeField] private TileBase _fertileTile;

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

    /// <summary>
    /// Проверка, что плитку можно удобрить
    /// </summary>
    /// <param name="_tilePosition"></param>
    public void CheckTile(Vector3Int _tilePosition)
    {
        TileBase _currentTile = _groundTileMap.GetTile(_tilePosition);
        
        if (_currentTile == _unfertileTile)
        {
            FertilizeTile(_tilePosition);
        }
    }

    /// <summary>
    /// Удобрение плитки
    /// </summary>
    /// <param name="_tilePosition"></param>
    private void FertilizeTile(Vector3Int _tilePosition)//Удобрение плитки
    {
        _groundTileMap.SetTile(_tilePosition, _fertileTile);
        GameManager.Instance.NewFertileTile();
    }
}