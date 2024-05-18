using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemigo : StateMachine
{
    public UnityEvent OnDead;
    Health healthComponent;

    protected override void Awake()
    {
        base.Awake();
        healthComponent = GetComponentInChildren<Health>();
        if (healthComponent)
        {
            healthComponent.NoHealth += Dead;
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    private void Dead()
    {
        OnDead.Invoke();
        Destroy(gameObject);
    }
}
