using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTransform : PlayerController
{
    [SerializeField] float transformCooldown;
    [SerializeField] PlayerController mecha1;
    [SerializeField] PlayerController mecha2;
    [SerializeField] bool isInvincible = true;

    Health health;
    PlayerController target;

    bool canTransform = true;

    protected override void Awake()
    {
        base.Awake();
        health = GetComponentInChildren<Health>();
    }

    public void TransformMecha(PlayerController currentController)
    {
        if (!canTransform)
        {
            personaje.CambiarEstado(currentController);
            return;
        }
        if (currentController == mecha1)
        {
            target = mecha2;
        }
        else
        {
            target = mecha1;
        }
        UpdateInvincibility(true && isInvincible);
        animator.SetTrigger("Transform");
    }

    public void EndTransformation()
    {
        UpdateInvincibility(false);
        personaje.CambiarEstado(target);
        animator.ResetTrigger("Attack");
        StartCoroutine(ControlTransformCooldown());
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

    IEnumerator ControlTransformCooldown()
    {
        Debug.Log("Transformación desactivada");
        canTransform = false;
        yield return new WaitForSeconds(transformCooldown);
        canTransform = true;
        Debug.Log("Transformación activada");
    }

}
