using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    
    [Header("UI")]
    public GameObject pausePanel;
    
    private bool isPaused = false;
    
    void Awake()
    {
        instance = this;
    }
    
    void Update()
    {
        // Клавиша Escape для паузы
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    
    public void TogglePause()
    {
        isPaused = !isPaused;
        
        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }
    
    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Останавливаем время
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Возвращаем время
    }
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Важно вернуть время перед загрузкой
        PlayerPrefs.Save(); // ← принудительно записываем на диск
        SceneManager.LoadScene("MainMenu");
    }
    
    public bool IsPaused()
    {
        return isPaused;
    }
}