using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTransform : PlayerController
{
    [SerializeField] PlayerController mecha1;
    [SerializeField] PlayerController mecha2;
    [SerializeField] bool isInvincible = true;

    Animator animator;
    Collider2D hurtbox;
    PlayerController target;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Health health = GetComponentInChildren<Health>();
        hurtbox = health.GetComponent<Collider2D>();
    }

    public override void Move(InputValue inputValue)
    {
        throw new System.NotImplementedException();
    }

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        UpdateInvincibility(true);
    }

    public void Transform(PlayerController currentController)
    {
        if(currentController == mecha1)
        {
            target = mecha2;
        }
        else
        {
            target = mecha1;
        }
    }

    public override void Salir()
    {
        base.Salir();
        UpdateInvincibility(false);
    }

    private void UpdateInvincibility(bool value)
    {
        hurtbox.enabled = !value;
    }

}
