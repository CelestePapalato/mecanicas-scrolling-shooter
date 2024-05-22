using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : StateMachine
{
    [SerializeField] int points;
    public UnityAction<Enemy> OnDead;
    public UnityAction<Enemy> Destroyed;
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
    }

    private void OnDestroy()
    {
        OnDead?.Invoke(this);
    }
}
