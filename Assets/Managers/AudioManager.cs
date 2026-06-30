using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    private AudioSource audioSource;
    private bool isMusicOn = true;
    
    void Awake()
    {
        // Синглтон — не уничтожается при загрузке новых сцен
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        audioSource = GetComponent<AudioSource>();
        
        // Загружаем настройку музыки
        if (PlayerPrefs.HasKey("MusicOn"))
        {
            isMusicOn = PlayerPrefs.GetInt("MusicOn") == 1;
        }
        
        UpdateMusicState();
    }
    
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
        UpdateMusicState();
    }
    
    public bool IsMusicOn()
    {
        return isMusicOn;
    }
    
    private void UpdateMusicState()
    {
        if (audioSource != null)
        {
            audioSource.mute = !isMusicOn;
        }
    }
}