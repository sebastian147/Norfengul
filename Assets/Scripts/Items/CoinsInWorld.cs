using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsInWorld : MonoBehaviour
{
    public int valor;
    private InventoryManager playerInventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<InventoryManager>();
            if (playerInventory != null)
            {
                playerInventory.Coins = playerInventory.Coins + valor;
                Destroy(gameObject);
            }
        }
    }
}

