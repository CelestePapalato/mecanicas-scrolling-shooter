using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooterMech : PlayerController
{
    [SerializeField] Vector2 shotVector;

    Shooter shooterComponent;

    private void Awake()
    {
        shooterComponent = GetComponent<Shooter>();
    }

    public override void Move(InputValue inputValue) { }

    public override void Attack()
    {
        if (!isActive || !canAttack) { return; }
        Debug.Log("Ataco bambam");
        base.Attack();
    }

    public override void Evade()
    {
        if (!isActive || !canEvade) { return; }
        Debug.Log("Esquivo >:P >:P");
        base.Evade();
    }
}
