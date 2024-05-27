using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform spawnPoint;

    public bool ShootProjectile(Vector2 direction, float damageMultiplier)
    {
        if (Time.timeScale == 0)
        {
            return false;
        }
        if (!spawnPoint) { return false; }
        Projectile projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        if (projectile)
        {
            projectile.Direction = direction;
            return true;
        }
        Damage projectileDamage = projectile.GetComponent<Damage>();
        if(projectileDamage) { projectileDamage.DamageMultiplier = damageMultiplier; }
        return false;
    }

    public bool ShootProjectile(float degreesAngle, float damageMultiplier)
    {
        if (Time.timeScale == 0)
        {
            return false;
        }
        Vector2 direction = new Vector2();
        direction.x = Mathf.Cos(degreesAngle * Mathf.Deg2Rad);
        direction.y = Mathf.Sin(degreesAngle * Mathf.Deg2Rad);
        return ShootProjectile(direction, damageMultiplier);
    }
}
