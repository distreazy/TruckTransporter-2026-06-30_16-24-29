using UnityEngine;

public class InitManager : MonoBehaviour
{
    void Awake()
    {
        // При первом запуске разблокируем Level 1
        if (!PlayerPrefs.HasKey("Level_1_Unlocked"))
        {
            PlayerPrefs.SetInt("Level_1_Unlocked", 1);
            PlayerPrefs.Save();
        }
    }
}