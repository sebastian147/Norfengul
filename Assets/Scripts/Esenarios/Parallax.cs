using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length; // The width of the sprite.
    private float startPos; // The initial X position of the object.
    [SerializeField] private GameObject cam; // Reference to the camera.
    [SerializeField] private float parallaxEffect; // Parallax effect factor.

    // Start is called before the first frame update.
    // Initializes the start position and sprite length, and finds the camera.
    void Start()
    {
        startPos = transform.position.x; // Saves the initial X position.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); // Gets the sprite width.
        if (spriteRenderer != null)
        {
            length = spriteRenderer.bounds.size.x;
        }
        else
        {
            Debug.LogError("The object doesn't have a SpriteRenderer component.");
        }
    }

    // Applies the parallax effect to the object.
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect)); // Calculates the temporary position of the background.
        float distance = (cam.transform.position.x * parallaxEffect); // Calculates the distance to move based on the parallax effect.

        Vector2 newPosition = new Vector2(startPos + distance, transform.position.y);
        // Snap position to the nearest pixel.

        transform.position = newPosition;
        // If the camera moves beyond the sprite, adjusts the start position to create an infinite scrolling effect.
        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}