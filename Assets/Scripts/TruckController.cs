using UnityEngine;

public class TruckController : MonoBehaviour
{
    public WheelJoint2D wheelBack;
    public WheelJoint2D wheelFront;
    
    public float maxSpeed = 300f;        // Максимальная скорость мотора
    public float acceleration = 300f;    // Ускорение (как быстро набираем обороты)
    public float motorForce = 1500f;     // Сила мотора
    public float brakeForce = 2000f;     // Сила торможения
    
    private JointMotor2D motorBack;
    private JointMotor2D motorFront;
    private float currentMotorSpeed = 0f; // Текущая скорость мотора
    
    void Start()
    {
        motorBack = wheelBack.motor;
        motorFront = wheelFront.motor;
    }
    
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        
        // ПЛАВНО меняем скорость мотора
        float targetSpeed = moveInput * maxSpeed;
        currentMotorSpeed = Mathf.MoveTowards(currentMotorSpeed, targetSpeed, acceleration * Time.deltaTime);
        
        // Турбо (Shift)
        float turboMultiplier = 1f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            turboMultiplier = 1.5f;
        }
        
        motorBack.motorSpeed = currentMotorSpeed * turboMultiplier;
        motorFront.motorSpeed = currentMotorSpeed * turboMultiplier;
        
        // Тормоз (пробел)
        if (Input.GetKey(KeyCode.Space))
        {
            // Резко сбрасываем скорость мотора
            currentMotorSpeed = Mathf.MoveTowards(currentMotorSpeed, 0f, acceleration * 2f * Time.deltaTime);
            motorBack.motorSpeed = 0f;
            motorFront.motorSpeed = 0f;
            motorBack.maxMotorTorque = brakeForce;
            motorFront.maxMotorTorque = brakeForce;
        }
        else
        {
            motorBack.maxMotorTorque = motorForce * turboMultiplier;
            motorFront.maxMotorTorque = motorForce * turboMultiplier;
        }
        
        wheelBack.motor = motorBack;
        wheelFront.motor = motorFront;
        wheelBack.useMotor = true;
        wheelFront.useMotor = true;
    }
}