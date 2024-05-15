using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHumanoidMech : PlayerController
{
    [SerializeField] float maxSpeed;

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        if (movement)
        {
            movement.MaxSpeed = maxSpeed;
        }
    }

    public override void OnAttack()
    {
        if (!isActive) { return; }
        Debug.Log("Ataco bambam");
    }

    public override void OnEvade()
    {
        if (!isActive) { return; }
        Debug.Log("Esquivo esquivo");
    }
}
