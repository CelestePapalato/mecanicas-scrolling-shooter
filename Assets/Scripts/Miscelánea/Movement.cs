using System;
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

    [Header("Movement smoothing")]
    [SerializeField]
    [Range(0, .15f)] float smoothTime;

    [Header("Debug")]
    [SerializeField]
    float currentSpeed = 0;
    [SerializeField]
    float speedMultiplier = 1;

    public float SpeedMultiplier { get => speedMultiplier; set => speedMultiplier = value; }

    public float MaxSpeed
    {
        get => maxSpeed * speedMultiplier;
        set
        {
            if (value > 0)
            {
                float diff = maxSpeed / value;
                maxSpeed = value;
                acceleration *= diff;
                decceleration *= diff;
            }
            if (value == 0)
            {
                maxSpeed = 0;
            }
        }
    }
    public float Acceleration
    {
        get => acceleration * speedMultiplier; private set { acceleration = (value > 0) ? value : acceleration; }
    }
    public float Decceleration
    {
        get => decceleration * speedMultiplier; private set { decceleration = (value > 0) ? value : decceleration; }
    }


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
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        Vector2 targetVelocity = currentDirection * MaxSpeed;
        float _acceleration = 0;
        float difference = targetVelocity.magnitude - currentSpeed;
        if (!ExtendedMaths.Approximately(difference, 0, 0.0001f))
        {
            if (difference > 0)
            {
                _acceleration = Mathf.Min(Acceleration * Time.fixedDeltaTime, difference);
            }
            else if (difference < 0)
            {
                _acceleration = Mathf.Max(-Decceleration * Time.fixedDeltaTime, difference);
            }
        }
        currentSpeed += _acceleration;
        if (targetVelocity.magnitude > 0)
        {
            if (currentVelocity != Vector2.zero)
            {
                currentVelocity = Vector2.SmoothDamp(currentVelocity, currentDirection.normalized, ref DAMP_currentVelocity, smoothTime);
            }
            else
            {
                currentVelocity = currentDirection.normalized;
            }
        }
        rb.MovePosition(rb.position + currentVelocity * currentSpeed * Time.deltaTime);
    }

    public void DontMove()
    {
        currentSpeed = 0;
        currentDirection = Vector2.zero;
    }
}
