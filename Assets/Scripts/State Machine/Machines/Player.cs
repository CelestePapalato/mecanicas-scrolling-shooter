using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : StateMachine, IBuffable
{
    float ogMaxSpeed;
    float ogAcceleration;
    float ogDecceleration;

    Health healthComponent;
    Movement movement;

    public UnityEvent OnDead;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        getOGMovementParameters();
        healthComponent = GetComponent<Health>();
        if (!healthComponent)
        {
            healthComponent = GetComponentInChildren<Health>();
        }
        healthComponent.NoHealth += Dead;
    }

    public void Accept(IBuff buff)
    {
        if (buff == null) return;
        buff.Buff(healthComponent);
        buff.Buff(this);
    }

    private void Dead()
    {
        movement.Direction = Vector2.zero;
        OnDead.Invoke();
        estadoActual.Salir();
        this.enabled = false;
    }

    public void SpeedPowerUp(float multiplier, float time)
    {
        StopCoroutine(nameof(SpeedPowerUpEnabler));
        multiplier = Mathf.Max(multiplier, 1f);
        if (multiplier == 1)
        {
            return;
        }
        modifySpeed(multiplier);
        StartCoroutine(SpeedPowerUpEnabler(time));
    }

    private void getOGMovementParameters()
    {
        ogMaxSpeed = movement.MaxSpeed;
        ogAcceleration = movement.Acceleration;
        ogDecceleration = movement.Decceleration;
    }

    private void resetMovementParameters()
    {
        movement.MaxSpeed = ogMaxSpeed;
        movement.Acceleration = ogAcceleration;
        movement.Decceleration = ogDecceleration;
    }

    private void modifySpeed(float multiplier)
    {
        movement.MaxSpeed = ogMaxSpeed * multiplier;
        movement.Acceleration = ogAcceleration * multiplier;
        movement.Decceleration = ogDecceleration * multiplier;
    }

    IEnumerator SpeedPowerUpEnabler(float time)
    {
        yield return new WaitForSeconds(time);
        resetMovementParameters();
    }

}