using UnityEngine;
using UnityEngine.UI;

public class MusicButtonManager : MonoBehaviour
{
    [Header("Sprites Button")]
    [SerializeField] private Sprite _musicOnSprite;
    [SerializeField] private Sprite _musicOffSprite;

    [Header("Button Image")]
    private Image _buttonImage;

    private void Start()
    {
        _buttonImage = GetComponent<Image>();
        _buttonImage.sprite = _musicOnSprite;
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
            AudioManager.Instance.ToggleMusic();
        }
    }

    private void SwitchImage()
    {
        if (_buttonImage.sprite == _musicOnSprite)
        {
            _buttonImage.sprite = _musicOffSprite;
        }
        else
        {
            _buttonImage.sprite = _musicOnSprite;
        }
    }
}
