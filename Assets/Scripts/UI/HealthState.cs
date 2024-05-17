using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Expression : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField][Tooltip("Porcentaje")] float lowHealth;
    [SerializeField]
    [Range(0f, 1f)] float dyeAmount;
    [SerializeField] float timeToDye;
    [SerializeField] Image expressionImage;
    [SerializeField] Material normal;
    [SerializeField] Material damaged;

    Image[] images;

    float currentDyeAmount;

    bool isLowHealth = false;

    bool lowHealthCoroutineRunning = false;

    private void Awake()
    {
        if (health)
        {
            health.HealthUpdate += IsLowHealth;
            health.Healed += Normal;
            health.Damaged += Damage;
        }
        images = GetComponentsInChildren<Image>();
        Init();
    }

    private void Init()
    {
        SetFlashAmount(0f);
        ChangeShaderTexture(normal);
    }

    private void IsLowHealth(int health, int maxHealth)
    {
        isLowHealth = health * 100f / maxHealth <= lowHealth;
        Debug.Log(isLowHealth);
        if (isLowHealth && !lowHealthCoroutineRunning)
        {
            ChangeShaderTexture(damaged);
            StopAllCoroutines();
            StartCoroutine(LowHealth());
        }
    }

    private void ChangeShaderTexture(Material mat)
    {
        expressionImage.material = mat;
    }

    private void Damage(int health, int maxHealth)
    {
        if (!isLowHealth || !lowHealthCoroutineRunning)
        {
            ChangeShaderTexture(damaged);
            StartCoroutine(DamageFlasher(dyeAmount, 0f, true));
        }
    }

    private void Normal(int health, int maxHealth)
    {
        if(!isLowHealth)
        {
            ChangeShaderTexture(normal);
            StopAllCoroutines();
            SetFlashAmount(0);
            lowHealthCoroutineRunning = false;
        }
    }

    IEnumerator DamageFlasher(float startAmount, float endAmount, bool changeSpriteAtEnd)
    {
        float elapsedTime = 0f;
        while(elapsedTime < timeToDye)
        {
            elapsedTime += Time.deltaTime;

            currentDyeAmount = Mathf.Lerp(startAmount, endAmount, elapsedTime/ timeToDye);

            SetFlashAmount(currentDyeAmount);

            yield return null;
        }
        if (changeSpriteAtEnd)
        {
            ChangeShaderTexture(normal);
        }
    }

    IEnumerator LowHealth()
    {
        lowHealthCoroutineRunning = true;
        while (isLowHealth)
        {
            StartCoroutine(DamageFlasher(dyeAmount, 0f, false));
            yield return new WaitForSeconds(timeToDye);
            StartCoroutine(DamageFlasher(0f, dyeAmount, false));
            yield return new WaitForSeconds(timeToDye);
        }
        lowHealthCoroutineRunning = false;
    }

    private void SetFlashAmount(float flashAmount)
    {
        foreach(Image image in images)
        {
            image.material.SetFloat("_FlashAmount", flashAmount);
        }
        normal.SetFloat("_FlashAmount", flashAmount);
        damaged.SetFloat("_FlashAmount", flashAmount);
    }
}
