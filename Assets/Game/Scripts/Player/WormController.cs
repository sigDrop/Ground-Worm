using UnityEngine;
using UnityEngine.Tilemaps;

public class WormController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap boundaryTilemap;
    [SerializeField] private GameManager gameManager;

    // ������� ��������� � ����������� �����
    private Vector3Int _currentCell;
    private Vector3Int _currentDirection;
    private Vector3Int _nextDirection;
    private float _moveTimer;

    void Start()
    {
        // ������������� �������
        _currentCell = groundTilemap.WorldToCell(transform.position);
        transform.position = groundTilemap.GetCellCenterWorld(_currentCell);

        // ��������� ����������� �������� (������)
        _nextDirection = Vector3Int.right;
    }

    void Update()
    {
        // ��������� ����� ������
        HandleInput();
    }

    // ��������� ����������
    private void HandleInput()
    {
        bool moved = false;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _nextDirection = Vector3Int.up;
            moved = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _nextDirection = Vector3Int.down;
            moved = true;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _nextDirection = Vector3Int.left;
            moved = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _nextDirection = Vector3Int.right;
            moved = true;
        }

        if (moved)
        {
            Move();
        }
    }

    // �������� ������ ��������
    private void Move()
    {
        // ��������� ����� �����������
        _currentDirection = _nextDirection;

        // ������� ������� � �����
        Vector3Int targetCell = _currentCell + _currentDirection;

        // �������� ����������� ��������
        if (CanMoveTo(targetCell))
        {
            // ��������� �������
            _currentCell = targetCell;
            transform.position = groundTilemap.GetCellCenterWorld(_currentCell);

            // �������� ������
            gameManager.FertilizeTile(_currentCell);
        }
    }

    // �������� �����������
    private bool CanMoveTo(Vector3Int cell)
    {
        // ������ ��������� �� �������
        if (boundaryTilemap.HasTile(cell))
            return false;

        // ������ ��������� �� ���������� ������
        TileBase tile = groundTilemap.GetTile(cell);
        return tile != gameManager.fertilizedTile;
    }
}
