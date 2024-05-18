using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]

public class MoveTowards : Estado
{
    [SerializeField] Transform destination;
    [SerializeField] float tolerance;
    [SerializeField] Estado nextState;

    Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        Vector2 direction = destination.position - transform.position;
        movement.Direction = direction;
    }

    public override void ActualizarFixed()
    {
        base.ActualizarFixed();
        if(Mathf.Abs((destination.position - transform.position).magnitude) <= tolerance)
        {
            personaje.CambiarEstado(nextState);
        }
    }

    public override void Salir()
    {
        base.Salir();
        movement.Direction = Vector2.zero;
    }
}
