using System.Collections;
using UnityEngine;

/// <summary>
/// Controla el efecto de disolución en varios objetos de juego.
/// </summary>
public class Dissolve : MonoBehaviour
{
    [SerializeField] private GameObject[] t; // Objetos a disolver
    private Material[] materials; // Materiales de los objetos
    private bool isDissolving = false; // Indica si el efecto está activo
    private float fade = 1f; // Nivel de disolución (1 = visible, 0 = invisible)
    private bool active = false; // Controla la activación/desactivación del efecto

    /// <summary>
    /// Propiedad para activar/desactivar el efecto de disolución.
    /// </summary>
    public bool ActiveEffect
    {
        get => active;
        set
        {
            if (active == value) return; // Evita cambios redundantes
            active = value;
            if (active) isDissolving = true; // Inicia la disolución si se activa
        }
    }

    /// <summary>
    /// Inicializa los materiales de los objetos al inicio.
    /// </summary>
    private void Start()
    {
        materials = new Material[t.Length]; // Inicializa el array de materiales

        for (int i = 0; i < t.Length; i++)
        {
            // Utiliza TryGetComponent para obtener el material de manera segura
            if (t[i].TryGetComponent(out SpriteRenderer renderer))
            {
                materials[i] = renderer.material;
            }
            else
            {
                Debug.LogError($"El objeto {t[i].name} no tiene un SpriteRenderer.");
            }
        }
    }

    /// <summary>
    /// Controla el proceso de disolución en cada frame.
    /// </summary>
    private void Update()
    {
        if (!isDissolving) return; // Si no está disolviendo, no hace nada

        fade = Mathf.Max(fade - Time.deltaTime * 0.5f, 0f); // Reduce 'fade' y asegura que no sea negativo

        // Aplica el valor de fade a cada material
        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i] != null)
            {
                materials[i].SetFloat("_Fade", fade);
            }
        }//TODO BLOKING STRUCTURE

        if (fade == 0f) isDissolving = false; // Finaliza la disolución cuando fade llega a 0
    }

    /// <summary>
    /// Activa el efecto de disolución.
    /// </summary>
    public void TriggerDissolve()
    {
        ActiveEffect = true; // Activa la disolución
    }
}
