using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInWorld : MonoBehaviour
{
    public float cooldown = 0.0f;
    public Items itemsPickable;
    private SpriteRenderer spriteRenderer;
    private InventoryManager playerInventory;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        spriteRenderer.sprite = itemsPickable.itemSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && cooldown <= 0)
        {
            playerInventory = other.GetComponent<InventoryManager>();
            if (playerInventory != null)
            {
                int emptySlotIndex = playerInventory.FindEmptySlot();
                if (emptySlotIndex != -1)
                {
                    playerInventory.AddItem(itemsPickable);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Inventario lleno. No se puede agregar más items.");
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.fixedDeltaTime;
            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;

        }
        else
        {
            cooldown = 0;
            spriteRenderer.color = originalColor;
        }
    }
}

