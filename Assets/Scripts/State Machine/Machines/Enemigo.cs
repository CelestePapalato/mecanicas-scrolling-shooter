using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemigo : StateMachine
{
    [SerializeField] int points;
    public UnityAction OnDead;
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
        Destroy(gameObject);
        GameManager.instance.AddPoints(points);
        OnDead?.Invoke();
    }
}
