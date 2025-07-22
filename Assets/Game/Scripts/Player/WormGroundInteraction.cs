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

    public void CheckTile(Vector3Int _tilePosition)
    {
        TileBase _currentTile = _groundTileMap.GetTile(_tilePosition);
        
        if (_currentTile == _unfertileTile)
        {
            FertilizeTile(_tilePosition);
        }
    }

    private void FertilizeTile(Vector3Int _tilePosition)
    {
        _groundTileMap.SetTile(_tilePosition, _fertileTile);
        GameManager.Instance.NewFertileTile();
    }
}
