using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooterMech : PlayerController
{
    [SerializeField] float maxSpeed;
    [SerializeField] Vector2 shotVector;

    Shooter shooterComponent;

    private void Awake()
    {
        shooterComponent = GetComponent<Shooter>();
    }

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        if (movement)
        {
            movement.MaxSpeed = maxSpeed;
        }
    }

    protected override void OnMove(InputValue inputValue)
    {
        base.OnMove(inputValue);

    }

    protected override void OnAttack()
    {
        if (!isActive) { return; }
        Debug.Log("ATACOOOO SLAAAAASH");
    }

    protected override void OnEvade()
    {
        if (!isActive) { return; }
        Debug.Log("Esquivo >:P >:P");
    }
}