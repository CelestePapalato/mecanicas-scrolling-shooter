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
    PlayerInput playerInput;

    public UnityEvent OnDead;

    bool attackInput = false;
    bool evadeInput = false;

    private float damageMultiplier = 1f;
    public float DamageMultiplier {  get { return damageMultiplier; } }

    private void Awake()
    {
        movement = GetComponent<Movement>();
        healthComponent = GetComponent<Health>();
        playerInput = GetComponent<PlayerInput>();
        if (!healthComponent)
        {
            healthComponent = GetComponentInChildren<Health>();
        }
        healthComponent.HealthUpdate += OnDamage;
        healthComponent.NoHealth += Dead;
    }

    private void Update()
    {
        if (attackInput)
        {
            controller?.Attack();
        }
        if(evadeInput)
        {
            controller?.Evade();
        }
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
        Debug.Log("aarr");
        playerInput.enabled = false;
        this.enabled = false;
    }

    public void SpeedPowerUp(float multiplier, float time)
    {
        multiplier = Mathf.Max(multiplier, 1f);
        if (multiplier == 1)
        {
            return;
        }
        StopCoroutine(nameof(SpeedPowerUpEnabler));
        StartCoroutine(SpeedPowerUpEnabler(multiplier, time));
    }


    IEnumerator SpeedPowerUpEnabler(float multiplier, float time)
    {
        movement.SpeedMultiplier = multiplier;
        yield return new WaitForSeconds(time);
        movement.SpeedMultiplier = 1f;
    }

    public void DamagePowerUp(float multiplier, float time)
    {
        multiplier = Mathf.Max(multiplier, 1f);
        if (multiplier == 1f)
        {
            return;
        }
        StopCoroutine(nameof(DamagePowerUpEnabler));
        StartCoroutine(DamagePowerUpEnabler(multiplier, time));
    }

    IEnumerator DamagePowerUpEnabler(float multiplier, float time)
    {
        damageMultiplier = multiplier;
        yield return new WaitForSeconds(time);
        damageMultiplier = 1f;
    }

    private void OnMove(InputValue inputValue)
    {
        movement.Direction = inputValue.Get<Vector2>();
        controller?.Move(inputValue);
    }

    private void OnAttack()
    {
        attackInput = !attackInput;
    }

    private void OnEvade()
    {
        evadeInput = !evadeInput;
    }

    private void OnDamage(int health, int maxHealth)
    {
        estadoActual?.DaņoRecibido();
    }

    private void OnTransform()
    {
        Debug.Log("Por el poder del prisma lunar");
    }
}