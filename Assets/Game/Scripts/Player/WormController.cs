using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WormController : MonoBehaviour
{
    [Header("WormSettings")]
    [SerializeField] private GameObject _bodySegment;
    [SerializeField] private int _startCountSegments;
    [SerializeField] private float _moveDuration = .3f;

    [Header("References")]
    [SerializeField] private Tilemap _groundTileMap;
    [SerializeField] private Tilemap _boundaryTileMap;
    [SerializeField] private LayerMask _interactbleLayer;

    private List<Transform> _wormSegments = new List<Transform>();// 0 - Голова, остальные - сегменты тела

    private bool _isCanMove = true;
    private bool _isFalling = false;

    private void Start()
    {
        _wormSegments.Add(gameObject.transform);

        SnapToGrid();

        if (_startCountSegments != 0)
        {
            CreateSegmentsOnStart(_startCountSegments);
        }
    }

    private void CreateSegmentsOnStart(int _countSegments)
    {
        for (int i = 0; i < _countSegments; i++)
        {
            GameObject _newBodySegment = Instantiate(_bodySegment);

            _newBodySegment.transform.position = new Vector3(_wormSegments[i].position.x - 1, _wormSegments[i].position.y, _wormSegments[i].position.z);

            _wormSegments.Add(_newBodySegment.transform);
        }
    }

    private void Update()
    {
        HandleInput();
        CheckAndApplyGravity();
    }

    private void HandleInput()
    {
        if (_isCanMove && !_isFalling)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                TryMove(Vector3.up);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                TryMove(Vector3.left);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                TryMove(Vector3.down);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                TryMove(Vector3.right);
            }
        }
    }

    private void TryMove(Vector3 _direction)
    {
        Vector3 _nextHeadPosition = transform.position + _direction;

        Vector3Int _nextCell = _boundaryTileMap.WorldToCell(_nextHeadPosition);

        // Проверяем, что следующая плитка не граница
        if (_boundaryTileMap.HasTile(_nextCell))
        {
            return;
        }

        // Проверяем, что следующая плитка не сегмент
        for (int i = 1; i < _wormSegments.Count; i++)
        {
            if (_nextHeadPosition == _wormSegments[i].position)
            {
                return;
            }
        }

        Collider2D _appleColider = Physics2D.OverlapPoint(_nextHeadPosition, _interactbleLayer);

        if (_appleColider != null)
        {
            GameObject _interactbleObject = _appleColider.gameObject;
            if (_interactbleObject.tag == "Apple")
            {
                AddSegment();
                Destroy(_interactbleObject);
            }
        }    

        StartCoroutine(MoveSmoothly(_nextHeadPosition));

        WormGroundInteraction.Instance.CheckTile(_nextCell);
    }

    private IEnumerator MoveSmoothly(Vector3 _direction)
    {
        _isCanMove = false;

        // Сохраняем позиции ДО движения
        List<Vector3> previousPositions = new List<Vector3>();
        foreach (Transform segment in _wormSegments)
        {
            previousPositions.Add(segment.position);
        }

        // Анимируем все сегменты одновременно
        float elapsedTime = 0f;

        while (elapsedTime < _moveDuration)
        {
            // Голова двигается к новой позиции
            _wormSegments[0].position = Vector3.Lerp(previousPositions[0], _direction, elapsedTime / _moveDuration);

            // Все сегменты двигаются одновременно
            for (int i = 1; i < _wormSegments.Count; i++)
            {
                _wormSegments[i].position = Vector3.Lerp(previousPositions[i], previousPositions[i - 1], elapsedTime / _moveDuration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Точные позиции в конце
        _wormSegments[0].position = _direction;
        for (int i = 1; i < _wormSegments.Count; i++)
        {
            _wormSegments[i].position = previousPositions[i - 1];
        }

        SnapToGrid();

        yield return StartCoroutine(ApplyGravitySmoothly());

        _isCanMove = true;
    }
    
    private bool IsCanFallDown()
    {
        foreach (Transform _segment in _wormSegments)
        {
            Vector3 _belowPosition = _segment.position + Vector3.down;

            Vector3Int _cell = _boundaryTileMap.WorldToCell(_belowPosition);

            Collider2D _appleColider = Physics2D.OverlapPoint(_belowPosition, _interactbleLayer);

            if (_boundaryTileMap.HasTile(_cell) || _appleColider != null)
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator ApplyGravitySmoothly()
    {
        while (IsCanFallDown())
        {
            _isFalling = true;

            List<Vector3> previousPositions = new List<Vector3>();
            foreach (Transform segment in _wormSegments)
            {
                previousPositions.Add(segment.position);
            }

            List<Vector3> targetPositions = new List<Vector3>();
            foreach (Vector3 pos in previousPositions)
            {
                targetPositions.Add(pos + Vector3.down);
            }

            float elapsedTime = 0f;

            while (elapsedTime < _moveDuration)
            {
                for (int i = 0; i < _wormSegments.Count; i++)
                {
                    _wormSegments[i].position = Vector3.Lerp(previousPositions[i], targetPositions[i], elapsedTime / _moveDuration);
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            for (int i = 0; i < _wormSegments.Count; i++)
            {
                _wormSegments[i].position = targetPositions[i];
            }

            SnapToGrid();
            
        }
        _isFalling = false;
    }

    private void CheckAndApplyGravity()
    {
        if (_isFalling || !_isCanMove) return;

        if (IsCanFallDown())
        {
            StartCoroutine(ApplyGravitySmoothly());
        }
    }

    private void AddSegment()
    {
        int _indexLastSegment = _wormSegments.Count - 1;

        GameObject _newSegment = Instantiate(_bodySegment);

        _newSegment.transform.position = new Vector3(_wormSegments[_indexLastSegment].position.x - 1, _wormSegments[_indexLastSegment].position.y, _wormSegments[_indexLastSegment].position.z);

        _wormSegments.Add(_newSegment.transform);
    }

    private void SnapToGrid()
    {
        foreach (Transform _transformItem in _wormSegments)
        {
            Vector3Int _currentCell = _groundTileMap.WorldToCell(_transformItem.position);
            _transformItem.position = _groundTileMap.GetCellCenterLocal(_currentCell);
        }
    }
}