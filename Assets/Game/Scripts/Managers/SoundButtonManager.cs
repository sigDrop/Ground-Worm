using UnityEngine;
using UnityEngine.UI;

public class SoundButtonManager : MonoBehaviour
{
    [Header("Sprites Button")]
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;

    [Header("Button Image")]
    private Image _buttonImage;

    private void Start()
    {
        _buttonImage = GetComponent<Image>();
        _buttonImage.sprite = _soundOnSprite;
    }

    public void ButtonClick()
    {
        ToggleSound();

        SwitchImage();
    }

    private void ToggleSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleSFX();
        }
    }

    private void SwitchImage()
    {
        if (_buttonImage.sprite == _soundOnSprite)
        {
            _buttonImage.sprite = _soundOffSprite;
        }
        else
        {
            _buttonImage.sprite = _soundOnSprite;
        }
    }
}
