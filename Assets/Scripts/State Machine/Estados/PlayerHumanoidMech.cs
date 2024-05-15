using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHumanoidMech : PlayerController
{
    public override void Move(InputValue inputValue) { }

    public override void Attack()
    {
        if (!isActive) { return; }
        Debug.Log("ATACOOOO SLAAAAASH");
    }

    public override void Evade()
    {
        if (!isActive) { return; }
        Debug.Log("Esquivo esquivo");
    }
}
