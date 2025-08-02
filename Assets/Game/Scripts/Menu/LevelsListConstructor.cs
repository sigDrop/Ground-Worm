using System.Collections.Generic;
using UnityEngine;

public class LevelsListConstructor : MonoBehaviour
{
    public static LevelsListConstructor Instance {  get; private set; }

    [Header("Scroll View Content Ref")]
    [SerializeField] private Transform _buttonsContainer;

    [Header("Button Open Level Prefab")]
    [SerializeField] private GameObject _levelButtonPrefab;

    [SerializeField] private int _levelsCount;

    private bool _isInitialaized = false;

    private List<OpenLevelButton> _buttonLevelsList = new List<OpenLevelButton>();

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

        _buttonLevelsList.Add(_levelButtonLogic);

        bool isButtonLocked = _levelNumber > LevelsProgress.GetLastUnlockedLevel() ? true : false;

        _levelButtonLogic?.SetupButton(_levelNumber, isButtonLocked);
    }

    public void UnlockButton(int indexButton)
    {
        if (indexButton >= 0 &&  indexButton < _buttonLevelsList.Count)
        {
            _buttonLevelsList[indexButton].UnlockButton();
        }    
    }    
}