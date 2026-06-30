using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("UI")]
    public GameObject winPanel;
    public TMP_Text resultText;
    public TMP_Text cargoInfoText;
    
    [Header("Кнопки после финиша")]
    public GameObject restartButton;
    public GameObject menuButton;
    
    private int totalCargo = 0;
    private bool raceFinished = false;
    
    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
        if (restartButton != null) restartButton.SetActive(false);
        if (menuButton != null) menuButton.SetActive(false);
        
        GameObject[] cargoObjects = GameObject.FindGameObjectsWithTag("Cargo");
        totalCargo = cargoObjects.Length;
        Debug.Log("Всего груза в сцене: " + totalCargo);
    }
    
    void Update()
    {
        if (cargoInfoText != null && !raceFinished)
        {
            CargoChecker checker = FindObjectOfType<CargoChecker>();
            if (checker != null)
            {
                int remaining = checker.GetCargoRemaining();
                cargoInfoText.text = $"Груз: {remaining} / {totalCargo}";
                
                float pct = (float)remaining / totalCargo * 100f;
                if (pct <= 50f)
                    cargoInfoText.color = Color.red;
                else if (pct < 100f)
                    cargoInfoText.color = Color.yellow;
                else
                    cargoInfoText.color = Color.green;
            }
        }
    }
    
    public void FinishRace(int cargoRemaining)
    {
        if (raceFinished) return;
        raceFinished = true;
        
        Debug.Log("!!! FinishRace вызван !!! Груза: " + cargoRemaining + " из " + totalCargo);
        
        if (Timer.instance != null)
        {
            Timer.instance.StopTimer();
        }
        
        float percentage = (float)cargoRemaining / totalCargo * 100f;
        float remainingTime = Timer.instance != null ? Timer.instance.GetRemainingTime() : 0f;
        
        bool timeNotUp = (remainingTime > 0f);
        
        string message = "";
        Color color = Color.white;
        bool levelPassed = false;
        
        // Минимальное количество груза для этого уровня
        int minCargo = GetMinCargoForCurrentLevel();
        bool enoughCargo = (cargoRemaining >= minCargo);
        
        if (!timeNotUp)
        {
            message = "Время вышло!\nПопробуйте ещё раз.";
            color = Color.red;
        }
        else if (!enoughCargo)
        {
            message = $"Слишком много груза потеряно!\nДоехало {cargoRemaining} из {totalCargo} ящиков\nНужно минимум {minCargo}\nПоражение!";
            color = Color.red;
        }
        else if (percentage < 100f)
        {
            message = $"Хорошо, но можно лучше!\nДоехало {cargoRemaining} из {totalCargo} ящиков ({percentage:F0}%)\nУровень пройден!";
            color = Color.yellow;
            levelPassed = true;
            SaveLevelProgress();
        }
        else
        {
            message = $"Отлично! Все ящики доставлены!\nОсталось времени: {remainingTime:F0} сек.";
            color = Color.green;
            levelPassed = true;
            SaveLevelProgress();
        }
        
        ShowResult(message, color, levelPassed);
    }

    private int GetMinCargoForCurrentLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        
        switch (currentLevel)
        {
            case 1: return totalCargo - 1;     
            case 2: return totalCargo - 1; 
            case 3: return totalCargo - 1;     
            default: return totalCargo;
        }
    }
    
    private void SaveLevelProgress()
    {
        Debug.LogError("!!! SaveLevelProgress ВЫЗВАН !!!");
        
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Индекс сцены: " + currentLevel);
        
        if (currentLevel < 1 || currentLevel > 3)
        {
            Debug.LogError("Индекс сцены вне диапазона: " + currentLevel);
            return;
        }
        
        // Сохраняем напрямую
        PlayerPrefs.SetInt("Level_" + currentLevel + "_Passed", 1);
        
        if (currentLevel < 3)
        {
            PlayerPrefs.SetInt("Level_" + (currentLevel + 1) + "_Unlocked", 1);
            Debug.LogError("СОХРАНЕНО: Level_" + (currentLevel + 1) + "_Unlocked = 1");
        }
        
        if (currentLevel == 3)
        {
            PlayerPrefs.SetInt("GameCompleted", 1);
            Debug.LogError("СОХРАНЕНО: GameCompleted = 1");
        }
        
        // ПРИНУДИТЕЛЬНО СОХРАНЯЕМ НА ДИСК
        PlayerPrefs.Save();
        
        // ПРОВЕРЯЕМ СРАЗУ ЖЕ
        int savedValue = PlayerPrefs.GetInt("Level_" + (currentLevel + 1) + "_Unlocked", -999);
        Debug.LogError("ПРОВЕРКА: значение после сохранения = " + savedValue);
    }
    
    void ShowResult(string message, Color color, bool levelPassed)
    {
        if (winPanel != null) winPanel.SetActive(true);
        if (resultText != null)
        {
            resultText.text = message;
            resultText.color = color;
        }
        
        if (restartButton != null) restartButton.SetActive(true);
        if (menuButton != null) menuButton.SetActive(true);
    }
    
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        PlayerPrefs.Save(); // СОХРАНЯЕМ ПЕРЕД УХОДОМ
        Debug.LogError("ЗАГРУЖАЕМ MAINMENU, ДАННЫЕ СОХРАНЕНЫ");
        SceneManager.LoadScene("MainMenu");
    }
}