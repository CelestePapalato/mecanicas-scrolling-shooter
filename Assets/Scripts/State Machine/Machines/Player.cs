using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : StateMachine, IBuffable
{
    float ogMaxSpeed;
    float ogAcceleration;
    float ogDecceleration;

    Health healthComponent;
    Movement movement;
    PlayerController controller;

    public UnityEvent OnDead;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        healthComponent = GetComponent<Health>();
        if (!healthComponent)
        {
            healthComponent = GetComponentInChildren<Health>();
        }
        healthComponent.HealthUpdate += OnDamage;
        healthComponent.NoHealth += Dead;
    }

    public override void CambiarEstado(Estado nuevoEstado)
    {
        base.CambiarEstado(nuevoEstado);
        controller = (PlayerController) estadoActual;
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

    private void resetMovementParameters()
    {
        movement.SpeedMultiplier = 1;
    }

    private void modifySpeed(float multiplier)
    {
        movement.SpeedMultiplier = multiplier;
    }

    IEnumerator SpeedPowerUpEnabler(float time)
    {
        yield return new WaitForSeconds(time);
        resetMovementParameters();
    }

    private void OnMove(InputValue inputValue)
    {
        movement.Direction = inputValue.Get<Vector2>();
        if (controller)
        {
            controller.Move(inputValue);
        }
    }

    private void OnAttack()
    {
        if (controller)
        {
            controller.Attack();
        }
    }

    private void OnEvade()
    {
        if (controller)
        {
            controller.Evade();
        }
    }

    private void OnDamage(int health, int maxHealth)
    {
        estadoActual.DañoRecibido();
    }

    private void OnTransform()
    {
        Debug.Log("Por el poder del prisma lunar");
    }
}