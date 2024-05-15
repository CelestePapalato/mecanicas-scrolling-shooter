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
    [SerializeField]
    [Range(0, .15f)] float smoothTime;
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float decceleration;
    [SerializeField]
    float currentMaxSpeed;
    [SerializeField]
    float currentAcceleration;
    [SerializeField]
    float currentDecceleration;
    [SerializeField]
    float currentSpeed = 0;

    public float MaxSpeed { get => maxSpeed; set { currentMaxSpeed = Mathf.Max(maxSpeed, value); } }
    public float Acceleration { get => acceleration; set { currentAcceleration = Mathf.Max(maxSpeed, value); } }
    public float Decceleration { get => decceleration; set { currentDecceleration = Mathf.Max(maxSpeed, value); } }


    Vector2 currentDirection = Vector2.zero;
    Vector2 currentVelocity = Vector2.zero;
    Vector2 DAMP_currentVelocity;

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
        Vector2 targetVelocity = currentDirection * currentMaxSpeed;
        float _acceleration = 0;
        float difference = targetVelocity.magnitude - currentSpeed;
        if (!ExtendedMaths.Approximately(difference, 0, 0.0001f))
        {
            if (difference > 0)
            {
                _acceleration = Mathf.Min(currentAcceleration * Time.fixedDeltaTime, difference);
            }
            else if (difference < 0)
            {
                _acceleration = Mathf.Max(-currentDecceleration * Time.fixedDeltaTime, difference);
            }
        }
        currentSpeed += _acceleration;
        Vector2 targetSpeed = currentDirection * currentSpeed * Time.fixedDeltaTime;
        if (targetVelocity.magnitude > 0)
        {
            //float t = currentSpeed / targetVelocity.magnitude;
            //currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, t);
            if(currentVelocity != Vector2.zero)
            {
                currentVelocity = Vector2.SmoothDamp(currentVelocity, currentDirection.normalized, ref DAMP_currentVelocity, smoothTime);
            }
            else
            {
                currentVelocity = currentDirection.normalized;
            }

            //currentVelocity = Vector2.Lerp(currentVelocity, currentDirection, smoothTime * Time.fixedDeltaTime);
        }
        Debug.Log(currentVelocity * currentSpeed);
        rb.MovePosition(rb.position + currentVelocity * currentSpeed * Time.deltaTime);
    }
}
