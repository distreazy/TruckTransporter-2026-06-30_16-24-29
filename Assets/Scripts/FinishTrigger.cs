using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что финиш пересек тягач
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Truck"))
        {
            // Находим CargoChecker в сцене
            CargoChecker checker = FindObjectOfType<CargoChecker>();
            
            if (checker != null)
            {
                int cargoRemaining = checker.GetCargoRemaining();
                GameManager.instance.FinishRace(cargoRemaining);
            }
            else
            {
                Debug.LogError("CargoChecker не найден в сцене!");
            }
        }
    }
}