using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Кнопки уровней")]
    public Button buttonLevel1;
    public Button buttonLevel2;
    public Button buttonLevel3;
    
    [Header("Иконки блокировки")]
    public GameObject lockIcon2;
    public GameObject lockIcon3;
    
    [Header("Панель завершения игры")]
    public GameObject gameCompletedPanel;
    
    void Start()
    {
        // НИКАКОГО СБРОСА!
        UpdateLevelButtons();
        
        // Проверяем, завершена ли игра
        if (PlayerPrefs.GetInt("GameCompleted", 0) == 1)
        {
            // Показываем панель, если ещё не показана
            // (или не показываем, если она на Level_3)
        }
    }
    
    void UpdateLevelButtons()
    {
        // Уровень 1 всегда доступен
        buttonLevel1.interactable = true;
        
        // Уровень 2
        bool level2Unlocked = PlayerPrefs.GetInt("Level_2_Unlocked", 0) == 1;
        buttonLevel2.interactable = level2Unlocked;
        if (lockIcon2 != null) lockIcon2.SetActive(!level2Unlocked);
        
        // Уровень 3
        bool level3Unlocked = PlayerPrefs.GetInt("Level_3_Unlocked", 0) == 1;
        buttonLevel3.interactable = level3Unlocked;
        if (lockIcon3 != null) lockIcon3.SetActive(!level3Unlocked);
        
        Debug.Log("Уровень 2: " + (level2Unlocked ? "Открыт" : "Закрыт"));
        Debug.Log("Уровень 3: " + (level3Unlocked ? "Открыт" : "Закрыт"));
    }
    
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    
    public void ToggleMusic()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.ToggleMusic();
        }
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}