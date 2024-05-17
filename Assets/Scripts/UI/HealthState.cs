using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Expression : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField][Tooltip("Porcentaje")] float lowHealth;
    [SerializeField] Image expressionImage;
    [SerializeField] Sprite normal;
    [SerializeField] Sprite damaged;

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
        if(expressionImage != null )
        {
            Normal();
        }
    }

    private void OnHealthUpdate(int health, int maxhealth)
    {
        if(health/maxhealth <= lowHealth)
        {

        }
    }

    private void changeShaderTexture(Texture2D tex)
    {
        expressionImage.material.mainTexture = tex;
    }

    private void Damage()
    {
        changeShaderTexture(damaged.texture);
    }

    private void Normal()
    {
        changeShaderTexture(normal.texture);
    }
}
