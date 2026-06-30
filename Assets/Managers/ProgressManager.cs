using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager instance;
    
    private static bool isFirstLaunch = true;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Сбрасываем прогресс только при ПЕРВОМ запуске игры
            if (isFirstLaunch)
            {
                ResetProgress();
                isFirstLaunch = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        
        // Разблокируем только первый уровень
        PlayerPrefs.SetInt("Level_1_Unlocked", 1);
        PlayerPrefs.Save();
        
        Debug.Log("Прогресс сброшен при запуске игры. Открыт только Уровень 1");
    }
}