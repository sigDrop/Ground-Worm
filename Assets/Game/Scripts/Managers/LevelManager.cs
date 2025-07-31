using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("List LevelData")]
    [SerializeField] private List<LevelData> _levelsData;
    
    private int _currentLevel = 0;

    public int GetCurrentLevel => _currentLevel;

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

    public void LoadLevelByIndex(int _levelIndex)
    {
        if (_levelIndex < _levelsData.Count && _levelIndex >= 0)
        {
            _currentLevel = _levelIndex;

            LevelLoader.Instance.LoadLevel(_levelsData[_currentLevel], _currentLevel);
            UIManager.Instance.UpdateLevelNumberToolTip(_currentLevel + 1);
        }
    }

    

    public void LoadNextLevel()
    {
        if (_currentLevel + 1 < _levelsData.Count)
        {
            LoadLevelByIndex(_currentLevel + 1);
        }
        else
        {
            UIManager.Instance.ShowLevelListPanel();
        }
    }
}
