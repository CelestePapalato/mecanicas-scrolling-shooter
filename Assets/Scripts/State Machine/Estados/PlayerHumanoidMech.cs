using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHumanoidMech : PlayerController
{
    [SerializeField]
    Damage sword;

    private void Awake()
    {
        if(!sword)
        {
            sword = GetComponentInChildren<Damage>();
        }
    }

    public override void Move(InputValue inputValue) { }

    public override void Attack()
    {
        if (!isActive || !canAttack) { return; }
        sword.DamageMultiplier = GetPlayerDamageMultiplier();
        Debug.Log("ATACOOOO SLAAAAASH");
        base.Attack();
    }

    public override void Evade()
    {
        if (!isActive || !canEvade) { return; }
        Debug.Log("Esquivo esquivo");
        base.Evade();
    }
}
