using UnityEngine;
using System.Collections.Generic;

public class CargoChecker : MonoBehaviour
{
    [Header("Настройки")]
    public string cargoTag = "Cargo";
    
    [Header("Звук падения груза")]
    public AudioClip cargoDropSound;
    
    private HashSet<GameObject> cargoInside = new HashSet<GameObject>();
    private int totalCargo = 0;
    private AudioSource audioSource;
    
    void Start()
    {
        // Находим или создаём AudioSource на ЭТОМ объекте
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Назначаем клип
        if (cargoDropSound != null && audioSource.clip == null)
        {
            audioSource.clip = cargoDropSound;
        }
        
        Debug.Log("CargoChecker AudioSource: " + (audioSource != null));
        Debug.Log("CargoChecker AudioClip: " + (audioSource.clip != null));
        
        // Считаем груз
        GameObject[] allCargo = GameObject.FindGameObjectsWithTag(cargoTag);
        totalCargo = allCargo.Length;
        
        foreach (GameObject cargo in allCargo)
        {
            cargoInside.Add(cargo);
        }
        
        Debug.Log("Всего груза: " + totalCargo);
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(cargoTag))
        {
            GameObject cargo = other.gameObject;
            
            if (cargoInside.Contains(cargo))
            {
                cargoInside.Remove(cargo);
                
                // === ЗВУК ПАДЕНИЯ ===
                PlayDropSound();
                
                Debug.Log("Ящик выпал! Осталось: " + cargoInside.Count + " из " + totalCargo);
                
                // Меняем цвет на красный
                SpriteRenderer sr = cargo.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = Color.red;
                }
            }
        }
    }
    
    void PlayDropSound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
            Debug.Log("ЗВУК ПАДЕНИЯ ПРОИГРАН");
        }
        else
        {
            Debug.LogError("НЕТ ЗВУКА! AudioSource: " + (audioSource != null) + ", Clip: " + (audioSource?.clip != null));
        }
    }
    
    public int GetCargoRemaining()
    {
        return cargoInside.Count;
    }
    
    public int GetTotalCargo()
    {
        return totalCargo;
    }
    
    public float GetCargoPercentage()
    {
        if (totalCargo == 0) return 0f;
        return (float)cargoInside.Count / totalCargo * 100f;
    }
}