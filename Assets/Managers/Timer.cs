using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    
    [Header("UI")]
    public TMP_Text timerText;
    
    [Header("Настройки")]
    public float timeLimit = 120f; // 2 минуты на уровень
    
    private float currentTime;
    private bool isRunning = false;
    private bool timeUp = false;
    
    void Awake()
    {
        instance = this;
        currentTime = timeLimit;
    }
    
    void Start()
    {
        StartTimer();
    }
    
    void Update()
    {
        if (isRunning && !timeUp)
        {
            currentTime -= Time.deltaTime;
            UpdateDisplay();
            
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                TimeUp();
            }
        }
    }
    
    void UpdateDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        
        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            
            // Красный цвет, если мало времени
            if (currentTime <= 30f)
                timerText.color = Color.red;
            else if (currentTime <= 60f)
                timerText.color = Color.yellow;
            else
                timerText.color = Color.white;
        }
    }
    
    void TimeUp()
    {
        timeUp = true;
        isRunning = false;
        
        // Проигрыш по времени
        GameManager.instance.FinishRace(0); // 0 груза = поражение
        Debug.Log("Время вышло!");
    }
    
    public void StartTimer()
    {
        isRunning = true;
        timeUp = false;
        currentTime = timeLimit;
    }
    
    public void StopTimer()
    {
        isRunning = false;
    }
    
    public float GetRemainingTime()
    {
        return currentTime;
    }
    
    public bool IsTimeUp()
    {
        return timeUp;
    }
}