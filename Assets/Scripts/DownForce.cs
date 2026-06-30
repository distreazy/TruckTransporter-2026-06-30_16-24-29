using UnityEngine;

public class DownForce : MonoBehaviour
{
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        // Постоянно давим вниз
        rb.AddForce(Vector2.down * rb.mass * 15f, ForceMode2D.Force);
    }
}