using UnityEngine;

public class EngineSound : MonoBehaviour
{
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        // Если AudioSource нет — создаём
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("AudioSource создан автоматически");
        }
        
        Debug.Log("EngineSound запущен на объекте: " + gameObject.name);
        Debug.Log("AudioSource найден: " + (audioSource != null));
        Debug.Log("AudioClip назначен: " + (audioSource.clip != null));
    }
    
    void Update()
    {
        // Проверяем нажатия клавиш
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || 
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("КЛАВИША НАЖАТА! Включаю звук");
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.time = 2f; // ← Начинаем со 2-й секунды
                audioSource.loop = true;
                audioSource.Play();
                Debug.Log("Звук запущен");
            }
            else
            {
                Debug.LogError("НЕТ AudioSource или AudioClip!");
            }
        }
        
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || 
            Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            Debug.Log("КЛАВИША ОТПУЩЕНА! Выключаю звук");
            if (audioSource != null)
            {
                audioSource.Stop();
                Debug.Log("Звук остановлен");
            }
        }
    }
}