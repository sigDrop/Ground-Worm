using System.ComponentModel;
using System.Xml;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Menu UI Reference")]
    [SerializeField] private GameObject _menuUI;

    [Header("Start Game Panel")]
    [SerializeField] private GameObject _startGamePanel;

    [Header("Level List Panel")]
    [SerializeField] private GameObject _levelListPanel;

    [Header("Level Number ToolTip")]
    [SerializeField] private TMP_Text _levelNumberText;

    [Header("Victory Panel")]
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private TMP_Text _victoryText;
    [SerializeField] private TMP_Text _timeText;

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
        _menuUI.SetActive(true);
        _startGamePanel.SetActive(true);

        _levelListPanel.SetActive(false);
        _victoryPanel.SetActive(false);
    }

    public void ClickStartGame()
    {
        _levelListPanel.SetActive(true);

        _startGamePanel.SetActive(false);
    }

    public void ShowLevelListPanel()
    {
        _menuUI.SetActive(true);
        _levelListPanel.SetActive(true);
    }

    public void HideLevelListPanel()
    {
        _levelListPanel.SetActive(false);
        _menuUI.SetActive(false);
    }

    public void ShowVictoryPanel(int _levelNumber, string _timeToWin)
    {
        _levelNumber++;
        _victoryPanel.SetActive(true);
        _victoryText.text = "Level " + _levelNumber + " Complete";
        _timeText.text = "Time: " + _timeToWin;
        AudioManager.Instance.PlayVictorySound();
    }

    public void HideVictoryPanel()
    {
        _victoryPanel.SetActive(false);
    }

    public void UpdateLevelNumberToolTip(int levelNumber)
    {
        _levelNumberText.text = levelNumber.ToString();
    }
}
