using UnityEngine;

public class LevelsListConstructor : MonoBehaviour
{
    [Header("Scroll View Content Ref")]
    [SerializeField] private Transform _buttonsContainer;

    [Header("Button Open Level Prefab")]
    [SerializeField] private GameObject _levelButtonPrefab;

    [SerializeField] private int _levelsCount;

    private bool _isInitialaized = false;

    private void Start()
    {
        InitializeLevelButtons();
    }

    private void InitializeLevelButtons()
    {
        if (_isInitialaized) return;

        for (int i = 0; i < _levelsCount; i++)
        {
            CreateLevelButton(i);
        }

        _isInitialaized = true;
    }

    private void CreateLevelButton(int _levelNumber)
    {
        GameObject _newButton = Instantiate(_levelButtonPrefab, _buttonsContainer);

        OpenLevelButton _levelButtonLogic = _newButton.GetComponent<OpenLevelButton>();

        _levelButtonLogic?.SetupButton(_levelNumber);
    }

}
