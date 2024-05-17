using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : StateMachine
{
    [SerializeField] Transform position;
    [SerializeField] float speed;
    [SerializeField] Estado estado;

    Vector2 destination;
    Health healthComponent;

    protected override void Awake()
    {
        base.Awake();
        healthComponent = GetComponentInChildren<Health>();
        if (position)
        {
            destination = position.position;
        }
        else
        {
            CambiarEstado(estado);
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    private void moveTowardsDestination()
    {
        Vector2 currentPosition = transform.position;
        if (currentPosition == destination)
        {
            CambiarEstado(estado);
            return;
        }

    }
}
