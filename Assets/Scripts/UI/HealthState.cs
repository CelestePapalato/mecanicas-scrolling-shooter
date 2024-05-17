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
    [SerializeField] Sprite normal;
    [SerializeField] Sprite damaged;

    Image[] images;
    Material[] materials;

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
        materials = new Material[images.Length];
        for (int i = 0; i < images.Length; i++)
        {
            materials[i] = images[i].material;
            materials[i].SetFloat("_FlashAmount", 0);
        }
    }

    private void IsLowHealth(int health, int maxHealth)
    {
        isLowHealth = health * 100f / maxHealth <= lowHealth;
        Debug.Log(isLowHealth);
        if (isLowHealth && !lowHealthCoroutineRunning)
        {
            StopAllCoroutines();
            StartCoroutine(LowHealth());
        }
    }

    private void ChangeShaderTexture(Texture2D tex)
    {
        expressionImage.material.mainTexture = tex;
    }

    private void Damage(int health, int maxHealth)
    {
        if (!isLowHealth || !lowHealthCoroutineRunning)
        {
            ChangeShaderTexture(damaged.texture);
            StartCoroutine(DamageFlasher(dyeAmount, 0f, true));
        }
    }

    private void Normal(int health, int maxHealth)
    {
        if(!isLowHealth)
        {
            ChangeShaderTexture(normal.texture);
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
            ChangeShaderTexture(normal.texture);
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
        for (int i = 0; i < materials.Length; i++)
        {

            materials[i].SetFloat("_FlashAmount", flashAmount);
        }
    }
}
