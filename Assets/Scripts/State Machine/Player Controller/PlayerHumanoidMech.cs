using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHumanoidMech : PlayerController
{
    [SerializeField]
    Damage sword;

    protected override void Awake()
    {
        base.Awake();
        if(!sword)
        {
            sword = GetComponentInChildren<Damage>();
        }
    }

    public override void Attack()
    {
        if (!isActive || !canAttack) { return; }
        sword.DamageMultiplier = GetPlayerDamageMultiplier();
        base.Attack();
    }

    public override void Evade()
    {
        if (!isActive || !canEvade) { return; }
        base.Evade();
    }
}
