using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase encargada de combinar varios sprites en uno solo.
/// </summary>
public class SpriteMerge : MonoBehaviour
{
    [SerializeField] private Sprite[] spritesToMerge = null; // Sprites que se van a combinar
    [SerializeField] private SpriteRenderer finalSpriteRenderer = null; // SpriteRenderer que mostrará el sprite final

    /// <summary>
    /// Método que se ejecuta al inicio. Llama a la función Merge.
    /// </summary>
    private void Start()
    {
        Merge(); // Llama al método que combina los sprites
    }

    /// <summary>
    /// Combina los sprites y actualiza el SpriteRenderer con el nuevo sprite.
    /// </summary>
    private void Merge()
    {
        // Libera recursos no utilizados
        Resources.UnloadUnusedAssets();

        // Crea una nueva textura de tamaño 320x320
        var newTex = new Texture2D(320, 320);

        // Inicializa todos los píxeles de la textura con un color transparente
        Color transparentColor = new Color(1, 1, 1, 0); // Blanco transparente
        for (int x = 0; x < newTex.width; x++)
        {
            for (int y = 0; y < newTex.width; y++)
            {
                newTex.SetPixel(x, y, transparentColor); // Establece cada píxel a transparente
            }
        }

        // Combina los sprites en la nueva textura
        foreach (var sprite in spritesToMerge)
        {
            var texture = sprite.texture; // Obtiene la textura del sprite actual
            int textureWidth = texture.width;
            int textureHeight = texture.height;

            for (int x = 0; x < textureWidth; x++)
            {
                for (int y = 0; y < textureHeight; y++)
                {
                    // Si el píxel es transparente en el sprite actual, toma el píxel existente en newTex
                    Color spriteColor = texture.GetPixel(x, y);
                    Color color = spriteColor.a == 0 ? newTex.GetPixel(x, y) : spriteColor;

                    newTex.SetPixel(x, y, color); // Actualiza el píxel en la nueva textura
                }
            }
        }

        // Aplica los cambios a la textura
        newTex.Apply();

        // Crea un nuevo sprite con la textura combinada
        var finalSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
        finalSprite.name = "Nuevo Sprite"; // Nombra el nuevo sprite
        finalSpriteRenderer.sprite = finalSprite; // Asigna el nuevo sprite al SpriteRenderer
    }
}