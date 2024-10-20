using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para sliders.

/// <summary>
/// Esta clase aplica un efecto de paralaje en elementos de fondo basados en el movimiento de la cámara.
/// El efecto se puede controlar en los ejes X y Y, y el fondo no se desplaza infinitamente.
/// Además, el efecto en el eje Y está limitado a una distancia máxima.
/// </summary>
public class Parallax : MonoBehaviour
{
    private float lengthX, lengthY; // Dimensiones del sprite en los ejes X e Y.
    private Vector2 startPos; // Posición inicial del objeto en X y Y.
    
    [SerializeField] private GameObject cam; // Referencia a la cámara.
    [SerializeField] private float parallaxEffectX; // Efecto de paralaje en el eje X.
    [SerializeField] private float parallaxEffectY; // Efecto de paralaje en el eje Y.

    [SerializeField] private float maxYOffset = 2.0f; // Máxima distancia que puede moverse en el eje Y.

    // Propiedades para acceder a los efectos de paralaje de forma controlada.
    public float ParallaxEffectX
    {
        get => parallaxEffectX;
        set => parallaxEffectX = Mathf.Clamp(value, 0f, 1f); // Asegura que el valor esté entre 0 y 1.
    }

    public float ParallaxEffectY
    {
        get => parallaxEffectY;
        set => parallaxEffectY = Mathf.Clamp(value, 0f, 1f); // Asegura que el valor esté entre 0 y 1.
    }

    // Start se ejecuta al inicio y calcula la posición inicial del objeto y las dimensiones del sprite.
    void Start()
    {
        startPos = transform.position; // Guarda la posición inicial del objeto.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); // Obtiene el sprite para determinar el tamaño.

        // Verifica que el objeto tenga un SpriteRenderer para calcular la longitud.
        if (spriteRenderer != null)
        {
            lengthX = spriteRenderer.bounds.size.x; // Longitud del sprite en X.
            lengthY = spriteRenderer.bounds.size.y; // Longitud del sprite en Y.
        }
        else
        {
            Debug.LogError("El objeto no tiene un componente SpriteRenderer.");
        }

        // Asegura que los efectos de paralaje estén en los rangos adecuados al inicio.
        ParallaxEffectX = parallaxEffectX;
        ParallaxEffectY = parallaxEffectY;
    }

    // FixedUpdate aplica el efecto de paralaje en cada frame fijo.
    void FixedUpdate()
    {
        // Calcula la posición temporal en X y la distancia en base al efecto de paralaje.
        float tempX = (cam.transform.position.x * (1 - parallaxEffectX));
        float distanceX = (cam.transform.position.x * parallaxEffectX);

        // Calcula la posición temporal en Y y la distancia en base al efecto de paralaje.
        float tempY = (cam.transform.position.y * (1 - parallaxEffectY));
        float distanceY = (cam.transform.position.y * parallaxEffectY);

        // Limita el movimiento en Y para que no se aleje demasiado de su posición inicial.
        float limitedY = Mathf.Clamp(startPos.y + distanceY, startPos.y - maxYOffset, startPos.y + maxYOffset);

        // Aplica la nueva posición calculada al objeto.
        transform.position = new Vector2(startPos.x + distanceX, limitedY);

        // Si la cámara se mueve más allá del tamaño del sprite en X, ajusta el inicio para crear el efecto de repetición.
        if (tempX > startPos.x + lengthX)
        {
            startPos.x += lengthX;
        }
        else if (tempX < startPos.x - lengthX)
        {
            startPos.x -= lengthX;
        }
    }
}
