using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Estado
{
    bool isActive = false;

    Movement movement;

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

    public void OnMove(InputValue inputValue)
    {
        if(movement &&  isActive) {
            movement.Direction = inputValue.Get<Vector2>();
        }
    }
}
