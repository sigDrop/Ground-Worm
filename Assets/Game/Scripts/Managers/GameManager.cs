using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [Header("Tile Settings")]
    public Tilemap groundTilemap;       // ���� � ������
    public TileBase unfertilizedTile;   // ������ �� ������������ ����
    public TileBase fertilizedTile;     // ������ �� ���������� ����

    private int _unfertilizedCount;

    void Start()
    {
        CountUnfertilizedTiles();
    }

    // ������� ���� ������������ ������ ��� ������
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

        Debug.Log($"��������� ���������� ������������ ������: {_unfertilizedCount}");
    }

    // ���������� �������� ��� ����������� �� ������
    public void FertilizeTile(Vector3Int position)
    {
        TileBase currentTile = groundTilemap.GetTile(position);

        if (currentTile == unfertilizedTile)
        {
            // �������� ���� �� ����������
            groundTilemap.SetTile(position, fertilizedTile);

            _unfertilizedCount--;
            Debug.Log($"�������� ������������: {_unfertilizedCount}");

            if (_unfertilizedCount <= 0)
            {
                Debug.Log("������! ��� ������ ��������!");
            }
        }
    }
}
