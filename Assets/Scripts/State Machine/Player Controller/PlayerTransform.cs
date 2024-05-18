using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTransform : PlayerController
{
    [SerializeField] PlayerController mecha1;
    [SerializeField] PlayerController mecha2;
    [SerializeField] bool isInvincible = true;

    Health health;
    PlayerController target;

    protected override void Awake()
    {
        base.Awake();
        health = GetComponentInChildren<Health>();
    }

    public override void Move(InputValue inputValue) { }

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        UpdateInvincibility(true && isInvincible);
    }

    public void TransformMecha(PlayerController currentController)
    {
        if (currentController == mecha1)
        {
            target = mecha2;
        }
        else
        {
            target = mecha1;
        }
        animator.SetTrigger("Transform");
    }

    public void EndTransformation()
    {
        UpdateInvincibility(false);
        personaje.CambiarEstado(target);
        animator.ResetTrigger("Attack");
    }

    public override void Salir()
    {
        base.Salir();
        UpdateInvincibility(false);
    }

    private void UpdateInvincibility(bool value)
    {
        health.UpdateInvincibility(value);
    }

}
