using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(AudioSource))]
public class ButtonSound : MonoBehaviour
{
    private Button button;
    private AudioSource audioSource;
    
    void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
        
        // Добавляем обработчик нажатия
        button.onClick.AddListener(() =>
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
        });
    }
}