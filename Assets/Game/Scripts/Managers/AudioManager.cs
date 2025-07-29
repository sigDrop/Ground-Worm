using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    [SerializeField] private AudioClip _backgroundMusic;

    [Header("SFX")]
    [SerializeField] private AudioClip _groundSound;
    [SerializeField] private AudioClip _victorySound;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _sfxAudioSource;

    private bool _isMusicMuted = false;
    private bool _isSFXMuted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SetupAudioSources();

        PlayBackgroundMusic();
    }

    private void SetupAudioSources()
    {
        _musicAudioSource.clip = _backgroundMusic;
        _musicAudioSource.loop = true;

        _sfxAudioSource.loop = false;
    }

    public void PlayBackgroundMusic()
    {
        if (!_isMusicMuted)
        {
            _musicAudioSource.Play();
        }
    }

    public void PlayGroundSound()
    {
        if (!_isSFXMuted)
        {
            _sfxAudioSource.PlayOneShot(_groundSound);
        }
    }

    public void PlayVictorySound()
    {
        if (!_isSFXMuted)
        {
            _sfxAudioSource.PlayOneShot(_victorySound);
        }
    }

    public void ToggleMusic()
    {
        _isMusicMuted = !_isMusicMuted;

        if (_isMusicMuted)
        {
            _musicAudioSource.Pause();
        }
        else
        {
            _musicAudioSource.UnPause();
        }
    }

    public void ToggleSFX()
    {
        _isSFXMuted = !_isSFXMuted;
    }

    public bool IsMusicMuted() => _isMusicMuted;
    public bool IsSFXMuted() => _isSFXMuted;
}