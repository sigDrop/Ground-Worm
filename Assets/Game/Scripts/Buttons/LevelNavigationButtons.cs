using UnityEngine;

public class LevelNavigationButtons : MonoBehaviour
{
    public void NextLevelClick()
    {
        LevelManager.Instance.LoadNextLevel();
        UIManager.Instance.HideVictoryPanel();
    }

    public void ReplayLevelClick()
    {
        int _currentLevel = LevelManager.Instance.GetCurrentLevel();
        LevelManager.Instance.LoadLevelByIndex(_currentLevel);
        UIManager.Instance.HideVictoryPanel();
    }

    public void OpenLevelListClick()
    {
        UIManager.Instance.ShowLevelListPanel();
        UIManager.Instance.HideVictoryPanel();
    }
}
