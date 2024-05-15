using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] protected Estado primerEstado;

    protected Estado estadoActual;

    void Start()
    {
        if (!primerEstado)
        {
            primerEstado = GetComponent<Estado>();
        }

        estadoActual = primerEstado;

        if (estadoActual)
        {
            CambiarEstado(estadoActual);
        }
        else
        {
            Debug.LogWarning("El State Machine " + name + "no posee ni encuentra un Estado al que llamar");
        }
    }

    void Update()
    {
        if (estadoActual)
        {
            estadoActual.Actualizar();
        }
    }

    void FixedUpdate()
    {
        if (estadoActual)
        {
            estadoActual.ActualizarFixed();
        }
    }

    public virtual void CambiarEstado(Estado nuevoEstado)
    {
        if (estadoActual)
        {
            estadoActual.Salir();
        }
        if (nuevoEstado)
        {
            estadoActual = nuevoEstado;
        }
        else
        {
            estadoActual = primerEstado;
        }
        if (!estadoActual)
        {
            estadoActual.Entrar(this);
        }
    }

    private void OnDisable()
    {
        estadoActual.Salir();
    }

    private void OnDestroy()
    {
        estadoActual.Salir();
    }
}

public abstract class Estado : MonoBehaviour
{
    protected StateMachine personaje;

    public virtual void Entrar(StateMachine personajeActual)
    {
        personaje = personajeActual;
    }
    public virtual void Salir() { }
    public virtual void Actualizar() { }
    public virtual void ActualizarFixed() { }
    public virtual void DañoRecibido() { }

    private void OnDisable()
    {
        if (!personaje) { return; }
        personaje.CambiarEstado(null);
    }

    private void OnDestroy()
    {
        if (!personaje) { return; }
        personaje.CambiarEstado(null);
    }
}