using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]

public class Turret : Estado
{
    [SerializeField] int projectilesPerShot;
    [SerializeField] float spreadAngle;
    [SerializeField] float fireRate;
    [SerializeField] float startAngle;
    [SerializeField] float endAngle;
    [SerializeField] float period;

    Shooter shooterComponent;

    [Header("Debug")]
    [SerializeField]
    float currentAngle;
    [SerializeField]
    float targetAngle;
    [SerializeField]
    float shootingAngle;

    float elapsedTime = 0f;

    private void Awake()
    {
        shooterComponent = GetComponent<Shooter>();
        currentAngle = startAngle;
        targetAngle = endAngle;
    }

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        elapsedTime = 0f;
        InvokeRepeating(nameof(StartShooting), 0, fireRate);
    }

    public override void Salir()
    {
        base.Salir();
        CancelInvoke();
    }

    public override void ActualizarFixed()
    {
        base.ActualizarFixed();
        float diff = elapsedTime;
        elapsedTime += Time.fixedDeltaTime;
        diff -= elapsedTime;
        diff *= -1;
        if (elapsedTime >= period)
        {
            if(Mathf.Abs(endAngle - startAngle) == 360)
            {
                currentAngle = startAngle;
                targetAngle = endAngle;
            }
            else
            {
                currentAngle = shootingAngle;
                targetAngle = (targetAngle == endAngle) ? startAngle : endAngle;
            }
            elapsedTime = diff;
        }
        shootingAngle = Mathf.Lerp(currentAngle, targetAngle, elapsedTime / period);
        elapsedTime = Mathf.Clamp(elapsedTime, 0f, period);
    }

    private void StartShooting()
    {
        float stepAngle = spreadAngle / (projectilesPerShot - 1);
        List<float> angles = new List<float>();
        float angle = shootingAngle - spreadAngle / 2;
        angles.Add(angle);
        for(int i = 1; i < projectilesPerShot; i++)
        {
            angle += stepAngle;
            angles.Add(angle);
        }
        foreach(float deg in angles)
        {
            shooterComponent.ShootProjectile(deg, 1);
        }
    }

}
