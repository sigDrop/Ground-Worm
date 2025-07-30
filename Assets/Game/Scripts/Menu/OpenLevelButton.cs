using TMPro;
using UnityEngine;

public class OpenLevelButton : MonoBehaviour
{
    [Header("Button Text Reference")]
    [SerializeField] private TMP_Text _buttonText;

    private int _levelIndex;

    public void SetupButton(int _index)
    {
        _levelIndex = _index;
        _buttonText.text = (_index + 1).ToString();
    }

    public void OpenLevel()
    {
        LevelManager.Instance.LoadLevelByIndex(_levelIndex);
        UIManager.Instance.HideLevelListPanel();
    }
}
