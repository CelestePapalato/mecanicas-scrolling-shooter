using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform spawnPoint;

    public bool shootProjectile(Vector2 direction)
    {
        if (!spawnPoint) { return false; }
        Projectile projectile = Instantiate(projectilePrefab, spawnPoint);
        if (projectile)
        {
            projectile.Direction = direction;
            return true;
        }
        return false;
    }

    public bool shootProjectile(float degreesAngle)
    {
        Vector2 direction = new Vector2();
        direction.x = Mathf.Cos(degreesAngle);
        direction.y = Mathf.Sin(degreesAngle);
        return shootProjectile(direction);
    }
}
