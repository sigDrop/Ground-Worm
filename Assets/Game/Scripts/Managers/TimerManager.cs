using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    [Header("UI Timer Text")]
    [SerializeField] private TMP_Text _timerText;

    private bool _isStop = true;
    private float _elapsedTime = 0f;

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
        }
    }

    private void Update()
    {
        CountTimer();

        UpdateTimerDisplay();
    }

    private void CountTimer()
    {
        if (!_isStop)
        {
            _elapsedTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        _isStop = false;
    }

    public void StopTimer()
    {
        _isStop = true;
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((_elapsedTime * 100f) % 100f);
        _timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }

    public void ResetTimer()
    {
        _elapsedTime = 0f;
        UpdateTimerDisplay();
    }
}
