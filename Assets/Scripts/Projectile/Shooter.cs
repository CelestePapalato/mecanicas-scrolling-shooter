using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform spawnPoint;

    public bool shootProjectile(Vector2 direction)
    {
        Projectile projectile = Instantiate(projectilePrefab, spawnPoint);
        if (projectile)
        {
            projectile.Direction = direction;
            return true;
        }
        return false;
    }
}
