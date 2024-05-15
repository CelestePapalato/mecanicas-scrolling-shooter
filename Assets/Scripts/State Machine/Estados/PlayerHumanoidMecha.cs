using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHumanoidMecha : PlayerController
{
    [SerializeField] float maxSpeed;

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        movement.MaxSpeed = maxSpeed;
    }

    public override void OnAttack()
    {
        Debug.Log("Ataco bambam");
    }

    public override void OnEvade()
    {
        Debug.Log("Esquivo esquivo");
    }
}
