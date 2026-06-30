using UnityEngine;

public class WheelLimiter : MonoBehaviour
{
    public Transform chassis;          // Кузов (Truck)
    public float maxDistance = 0.8f;   // Максимальное расстояние от точки крепления
    public float springForce = 50f;    // Сила возврата
    
    private Rigidbody2D wheelRb;
    private Vector2 anchorPoint;       // Точка крепления на кузове
    
    void Start()
    {
        wheelRb = GetComponent<Rigidbody2D>();
        // Запоминаем точку крепления (позиция колеса относительно Truck при старте)
        anchorPoint = chassis.InverseTransformPoint(transform.position);
    }
    
    void FixedUpdate()
    {
        // Текущая позиция колеса относительно кузова
        Vector2 currentLocalPos = chassis.InverseTransformPoint(transform.position);
        
        // Расстояние от точки крепления
        float distance = Vector2.Distance(currentLocalPos, anchorPoint);
        
        // Если колесо уехало слишком далеко — возвращаем
        if (distance > maxDistance)
        {
            Vector2 direction = (currentLocalPos - anchorPoint).normalized;
            Vector2 targetLocalPos = anchorPoint + direction * maxDistance;
            Vector2 targetWorldPos = chassis.TransformPoint(targetLocalPos);
            
            // Прикладываем силу к колесу в сторону кузова
            Vector2 correctionForce = ((Vector2)chassis.TransformPoint(anchorPoint) - (Vector2)transform.position).normalized * springForce;
            wheelRb.AddForce(correctionForce, ForceMode2D.Force);
        }
    }
    
    // Визуализация в редакторе
    void OnDrawGizmos()
    {
        if (chassis != null)
        {
            Gizmos.color = Color.yellow;
            Vector2 worldAnchor = chassis.TransformPoint(anchorPoint);
            Gizmos.DrawWireSphere(worldAnchor, maxDistance);
        }
    }
}