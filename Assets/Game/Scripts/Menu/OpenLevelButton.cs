using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenLevelButton : MonoBehaviour
{
    [Header("Button Sprite Type")]
    [SerializeField, Tooltip("Image Unlocked Level")] private Sprite _imageButtonUnlocked;
    [SerializeField, Tooltip("Image Locked Level")] private Sprite _imageButtonLocked;

    [Header("Button Text Reference")]
    [SerializeField] private TMP_Text _buttonText;

    private bool _isLocked = true;

    private int _levelIndex;

    public void SetupButton(int _index, bool isLocked)
    {
        Image buttonImgage = GetComponent<Image>();

        if (isLocked)
        {
            buttonImgage.sprite = _imageButtonLocked;
        }
        else
        {
            buttonImgage.sprite = _imageButtonUnlocked;
            _isLocked = false;
        }

        _levelIndex = _index;

        _buttonText.text = (_index + 1).ToString();
    }

    public void OpenLevel()
    {
        if (!_isLocked)
        {
            LevelManager.Instance.LoadLevelByIndex(_levelIndex);
            UIManager.Instance.HideLevelListPanel();
        }
    }

    public void UnlockButton()
    {
        Image buttonImgage = GetComponent<Image>();
        buttonImgage.sprite = _imageButtonUnlocked;
        _isLocked = false;
    }
}
