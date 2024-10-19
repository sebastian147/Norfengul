using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Controla el efecto de disolución en varios objetos de juego.
/// </summary>
public class Dissolve : MonoBehaviour
{
    [SerializeField] private GameObject[] t; // Objetos que tendrán el efecto de disolución
    private Material[] materials; // Materiales de los objetos a disolver
    private int i = 0; // Índice para recorrer los objetos

    private bool isDissolving = false; // Bandera que indica si el efecto de disolución está activo
    private double fade = 1f; // Controla el nivel de disolución (1 es visible, 0 es invisible)

    private bool active = false; // Bandera para activar o desactivar el efecto

    /// <summary>
    /// Propiedad que controla si el efecto de disolución está activo o no.
    /// </summary>
    public bool ActiveEffect
    {
        get => active;
        set
        {
            active = value;
            if (active)
            {
                isDissolving = true; // Si se activa, inicia la disolución
            }
        }
    }

    /// <summary>
    /// Método que se ejecuta al inicio. Inicializa los materiales de los objetos.
    /// </summary>
    void Start()
    {
        materials = new Material[t.Length]; // Inicializa el array de materiales
        for (int i = 0; i < t.Length; i++)
        {
            try
            {
                // Intenta obtener el material del SpriteRenderer del objeto actual
                materials[i] = t[i].GetComponent<SpriteRenderer>().material;
            }
            catch (Exception e)
            {
                // Si falla, imprime un error
                Debug.LogError($"Error en {i}: {t[i]} - {e}");
            }
        }
    }

    /// <summary>
    /// Método que se llama en cada frame. Controla la disolución.
    /// </summary>
    void Update()
    {
        if (isDissolving) // Si está disolviendo
        {
            fade -= Time.deltaTime * 0.5; // Reduce el valor de fade gradualmente

            if (fade <= 0f)
            {
                fade = 0f; // Asegura que fade no sea negativo
                isDissolving = false; // Detiene la disolución cuando fade llega a 0
            }

            // Aplica el valor de fade a cada material
            for (int i = 0; i < t.Length; i++)
            {
                materials[i].SetFloat("_Fade", (float)fade);
            }
        }
    }

    /// <summary>
    /// Activa el efecto de disolución.
    /// </summary>
    public void TriggerDissolve()
    {
        ActiveEffect = true; // Activa el efecto
    }
}