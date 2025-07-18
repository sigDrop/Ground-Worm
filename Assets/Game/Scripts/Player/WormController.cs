using UnityEngine;
using UnityEngine.Tilemaps;

public class WormController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap boundaryTilemap;
    [SerializeField] private GameManager gameManager;

    // Текущее положение в координатах сетки
    private Vector3Int _currentCell;
    private Vector3Int _currentDirection;
    private Vector3Int _nextDirection;
    private float _moveTimer;

    void Start()
    {
        // Инициализация позиции
        _currentCell = groundTilemap.WorldToCell(transform.position);
        transform.position = groundTilemap.GetCellCenterWorld(_currentCell);

        // Начальное направление движения (вправо)
        _nextDirection = Vector3Int.right;
    }

    void Update()
    {
        // Обработка ввода игрока
        HandleInput();
    }

    // Обработка управления
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

    // Основная логика движения
    private void Move()
    {
        // Применяем новое направление
        _currentDirection = _nextDirection;

        // Целевая позиция в сетке
        Vector3Int targetCell = _currentCell + _currentDirection;

        // Проверка возможности движения
        if (CanMoveTo(targetCell))
        {
            // Обновляем позицию
            _currentCell = targetCell;
            transform.position = groundTilemap.GetCellCenterWorld(_currentCell);

            // Удобряем плитку
            gameManager.FertilizeTile(_currentCell);
        }
    }

    // Проверка препятствий
    private bool CanMoveTo(Vector3Int cell)
    {
        // Нельзя двигаться на границы
        if (boundaryTilemap.HasTile(cell))
            return false;

        // Нельзя двигаться на удобренные плитки
        TileBase tile = groundTilemap.GetTile(cell);
        return tile != gameManager.fertilizedTile;
    }
}
