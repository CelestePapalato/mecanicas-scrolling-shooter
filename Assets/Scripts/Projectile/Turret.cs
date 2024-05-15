using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]

public class Turret : Estado
{
    [SerializeField] int projectilesPerShot;
    [SerializeField] float fireRate;
    [SerializeField] float startAngle;
    [SerializeField] float endAngle;
    [SerializeField] float rotationSpeed;

    Shooter shooterComponent;

    private void Awake()
    {
        shooterComponent = GetComponent<Shooter>();
    }

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
    }

    public override void Salir()
    {
        base.Salir();
    }
}
