using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int _currentLevel;
    private int _countTilesToWin;

    private bool _isLevelStart;

    public bool LevelIsStart => _isLevelStart;

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

    public void InitializeLevel(int levelNumber, int countUnfertileTiles)
    {
        _currentLevel = levelNumber;
        _countTilesToWin = countUnfertileTiles;
        StartLevel();
    }

    public void StartLevel()
    {
        TimerManager.Instance.ResetTimer();

        _isLevelStart = true;

        TimerManager.Instance.StartTimer();
    }

    public void NewFertileTile()
    {
        _countTilesToWin--;
        if (_countTilesToWin <= 0)
        {
            AllTilesFertile();
        }
    }

    private void AllTilesFertile()
    {
        _isLevelStart = false;
        TimerManager.Instance.StopTimer();
        UIManager.Instance.ShowVictoryPanel(_currentLevel, TimerManager.Instance.GetTime());
    }

    public void StopGame()
    {
        TimerManager.Instance.StopTimer();
        _isLevelStart = false;
    }
}