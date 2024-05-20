using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Octoenemigo : MonoBehaviour
{
    [SerializeField]
    int puntos;

    public UnityAction OnDead;

    int partesVivas = 0;

    private void Awake()
    {
        Enemigo[] partes = GetComponentsInChildren<Enemigo>();
        foreach(Enemigo enemigo in partes)
        {
            enemigo.OnDead += PartDestroyed;
        }
        partesVivas = partes.Length;
    }

    private void PartDestroyed()
    {
        Debug.Log(partesVivas);
        if(partesVivas <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.AddPoints(puntos);
            OnDead?.Invoke();
        }
    }
}
