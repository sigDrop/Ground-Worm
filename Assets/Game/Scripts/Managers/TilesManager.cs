using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesManager : MonoBehaviour
{
    public static TilesManager Instance { get; private set; }

    [Header("TileMaps References")]
    [SerializeField] private Tilemap _groundTileMap;
    [SerializeField] private Tilemap _boundaryTileMap;

    [Header("TileBases References")]
    [SerializeField] private TileBase _unfertileTile;
    [SerializeField] private TileBase _fertileTile;
    [SerializeField] private TileBase _boundaryTile;

    public Tilemap GroundTileMap => _groundTileMap;
    public Tilemap BoundaryTileMap => _boundaryTileMap;
    public TileBase UnfertileTile => _unfertileTile;
    public TileBase FertileTile => _fertileTile;
    public TileBase BoundaryTile => _boundaryTile;

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
}