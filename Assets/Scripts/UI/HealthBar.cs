using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// La clase HealthBar gestiona la barra de vida visual del jugador, 
/// permitiendo establecer la salud máxima y actualizar la salud actual.
/// </summary>
public class HealthBar : MonoBehaviour
{
    // El slider que representa la barra de vida
    [SerializeField]
    private Slider slider;

    /// <summary>
    /// Establece el valor máximo de salud y ajusta el slider a ese valor.
    /// </summary>
    /// <param name="health">El valor máximo de salud.</param>
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;  // Inicializa la barra con el valor máximo de salud
    }

    /// <summary>
    /// Actualiza el valor actual de salud en el slider.
    /// </summary>
    /// <param name="health">El valor actual de salud.</param>
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    // TODO: Considerar animar la transición de la barra de vida para mejorar la experiencia visual.
}