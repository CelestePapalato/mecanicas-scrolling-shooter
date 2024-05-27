using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNonVisible : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    bool wasOnCamera;
    bool shouldDestroy = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(!spriteRenderer) { Destroy(gameObject);}
    }

    void Update()
    {
        if (Time.time == 0)
        {
            return;
        }
        bool isVisible = spriteRenderer.isVisible;
        wasOnCamera = isVisible || wasOnCamera;
        shouldDestroy = wasOnCamera && !isVisible;
        if (shouldDestroy)
        {
            Destroy(gameObject);
        }
    }
}
