using UnityEngine;
using UnityEngine.Tilemaps;

public class WormGroundInteraction : MonoBehaviour
{
    public static WormGroundInteraction Instance { get; private set; }

    private Tilemap _groundTileMap;

    private TileBase _unfertileTile;
    private TileBase _fertileTile;

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

        _unfertileTile = TilesManager.Instance.UnfertileTile;
        _fertileTile = TilesManager.Instance.FertileTile;
    }

    /// <summary>
    /// Проверка, что плитку можно удобрить
    /// </summary>
    /// <param name="_tilePosition"></param>
    public void CheckTile(Vector3Int _tilePosition)
    {
        TileBase _currentTile = _groundTileMap.GetTile(_tilePosition);
        
        if (_currentTile == _fertileTile) return;

        FertilizeTile(_tilePosition);
    }

    /// <summary>
    /// Удобрение плитки
    /// </summary>
    /// <param name="_tilePosition"></param>
    private void FertilizeTile(Vector3Int _tilePosition)//Удобрение плитки
    {
        _groundTileMap.SetTile(_tilePosition, _fertileTile);
        GameManager.Instance.NewFertileTile();
        AudioManager.Instance.PlayGroundSound();
    }
}