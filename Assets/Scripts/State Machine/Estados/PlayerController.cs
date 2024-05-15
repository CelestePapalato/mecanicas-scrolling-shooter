using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerController : Estado
{
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
    }

    public override void Salir()
    {
        base.Salir();
        isActive = false;
    }

    protected virtual void OnMove(InputValue inputValue)
    {
        if(movement &&  isActive) {
            movement.Direction = inputValue.Get<Vector2>();
        }
    }

    protected abstract void OnAttack();

    protected abstract void OnEvade();
}
