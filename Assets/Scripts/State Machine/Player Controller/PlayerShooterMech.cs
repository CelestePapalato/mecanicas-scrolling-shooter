using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooterMech : PlayerController
{
    [SerializeField] Vector2 shotVector;

    Shooter shooterComponent;

    protected override void Awake()
    {
        base.Awake();
        shooterComponent = GetComponent<Shooter>();
    }

    public override void Move(InputValue inputValue) { }

    public override void Attack()
    {
        if (!isActive || !canAttack) { return; }
        shooterComponent?.ShootProjectile(shotVector, GetPlayerDamageMultiplier());
        base.Attack();
    }

    public override void Evade()
    {
        if (!isActive || !canEvade) { return; }
        base.Evade();
    }
}
