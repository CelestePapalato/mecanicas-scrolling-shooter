using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFlash : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] float timeToDye;

    Material flashMaterial;

    float currentDyeAmount;

    private void Awake()
    {
        if (health)
        {
            health.Damaged += Damage;
        }
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        flashMaterial = new Material(Shader.Find("Shader Graphs/Dyed Sprite"));
        flashMaterial.SetColor("_FlashColor", Color.white);
        spriteRenderer.material = flashMaterial;
        spriteRenderer.material.SetTexture("_MainTex", spriteRenderer.sprite.texture);
        SetFlashAmount(0);
    }

    private void Damage(int health, int maxHealth)
    {
        StopAllCoroutines();
        StartCoroutine(DamageFlasher(1f, 0f));
    }

    [ContextMenu("Start Flash")]
    private void StartFlash()
    {
        StopAllCoroutines();
        StartCoroutine(DamageFlasher(1f, 0f));
    }

    IEnumerator DamageFlasher(float startAmount, float endAmount)
    {
        float elapsedTime = 0f;
        while (elapsedTime < timeToDye)
        {
            elapsedTime += Time.deltaTime;

            currentDyeAmount = Mathf.Lerp(startAmount, endAmount, elapsedTime / timeToDye);

            SetFlashAmount(currentDyeAmount);
            yield return null;
        }
    }

    private void SetFlashAmount(float flashAmount)
    {
        flashMaterial.SetFloat("_FlashAmount", flashAmount);
    }

    private void OnDestroy()
    {
        Destroy(flashMaterial);
    }
}
