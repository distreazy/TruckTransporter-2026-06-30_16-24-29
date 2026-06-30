using UnityEngine;

public class CenterMassControl : MonoBehaviour
{
    public Rigidbody2D truckRigidbody;
    public float shiftAmount = 0.5f;
    public float shiftSpeed = 2f;
    
    private float currentShift = 0f;
    
    void Start()
    {
        if (truckRigidbody == null)
            truckRigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        float input = Input.GetAxis("Vertical");
        currentShift = Mathf.MoveTowards(currentShift, input * shiftAmount, shiftSpeed * Time.deltaTime);
        truckRigidbody.centerOfMass = new Vector2(currentShift, -0.8f);
    }
}