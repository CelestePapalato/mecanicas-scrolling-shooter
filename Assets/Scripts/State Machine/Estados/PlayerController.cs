using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerController : Estado
{
    [SerializeField] float maxSpeed;

    protected bool isActive = false;

    protected Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        isActive = true;
        if (movement)
        {
            movement.MaxSpeed = maxSpeed;
        }
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
    }

    public override void Salir()
    {
        base.Salir();
        isActive = false;
    }

    public abstract void Move(InputValue inputValue);

    public abstract void Attack();

    public abstract void Evade();
}
