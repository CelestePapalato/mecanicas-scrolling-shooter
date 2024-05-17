using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Expression : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] float lowHealth;
    [SerializeField] Image expressionImage;
    [SerializeField] Sprite normal;
    [SerializeField] Sprite damaged;
    [SerializeField] float timeDamaged;

    Image[] images;

    private void Awake()
    {
        if (health)
        {

            health.HealthUpdate += OnHealthUpdate;
            health.InvincibilityFinished += Normal;
            health.InvincibilityStarted += Damage;
        }
        images = GetComponents<Image>();
    }

    private void OnHealthUpdate(int health, int maxhealth)
    {

    }

    private void Damage()
    {
        expressionImage.sprite = damaged;
    }

    private void Normal()
    {
        expressionImage.sprite = normal;
    }
}
