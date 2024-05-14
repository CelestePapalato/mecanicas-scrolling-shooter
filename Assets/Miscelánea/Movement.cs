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
    Vector2 currentVelocity = Vector2.zero;

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
        Vector2 movement = Vector2.zero;
        Vector2 targetSpeed = currentDirection * currentMaxSpeed;
        float difference = targetSpeed.magnitude - currentVelocity.magnitude;
        if (!ExtendedMaths.Approximately(difference, 0, 0.0000001f))
        {
            float _acceleration = 0;
            if (difference < 0)
            {
                _acceleration = Mathf.Max(-currentDecceleration * Time.fixedDeltaTime, difference);
            }
            else
            {
                _acceleration = Mathf.Min(currentAcceleration * Time.fixedDeltaTime, difference);
            }
            difference = 1/difference;
            movement = targetSpeed - currentVelocity;
            movement *= difference * _acceleration;
        }
        currentVelocity += movement;
        rb.velocity += movement;
    }

}
