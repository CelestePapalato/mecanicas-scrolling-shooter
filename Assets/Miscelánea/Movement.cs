using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedMaths
{
    public static bool Approximately(float a, float b, float tolerance)
    {
        return Mathf.Abs(a - b) < tolerance;
    }
}


public class Movement : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float decceleration;

    float currentMaxSpeed;
    float currentAcceleration;
    float currentDecceleration;

    public float MaxSpeed { get => maxSpeed; set { currentMaxSpeed = Mathf.Max(maxSpeed, value); } }
    public float Acceleration { get => acceleration; set { currentAcceleration = Mathf.Max(maxSpeed, value); } }
    public float Decceleration { get => decceleration; set { currentDecceleration = Mathf.Max(maxSpeed, value); } }


    Vector2 currentDirection = Vector2.zero;
    public Vector2 Direction
    {
        get => currentDirection;

        set => currentDirection = Vector2.ClampMagnitude(value, 1);

    }

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMaxSpeed = maxSpeed;
        currentAcceleration = acceleration;
        currentDecceleration = decceleration;
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        Vector2 movementVector = Vector2.zero;
        Vector2 targetSpeed = currentDirection * currentMaxSpeed;
        Vector2 currentSpeed = rb.velocity;
        currentSpeed.y = 0;
        float difference = targetSpeed.magnitude - currentSpeed.magnitude;
        float _acceleration;
        if (!ExtendedMaths.Approximately(difference, 0, 0.01f))
        {
            if (difference > 0)
            {
                _acceleration = Mathf.Min(currentAcceleration * Time.fixedDeltaTime, difference);
            }
            else
            {
                _acceleration = Mathf.Max(-currentDecceleration * Time.fixedDeltaTime, difference);
            }
            difference = 1f / difference;
            movementVector = targetSpeed - currentSpeed;
            movementVector *= difference * _acceleration;
        }
        rb.velocity += movementVector;
    }

}
