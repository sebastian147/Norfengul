using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class InventoryManager : MonoBehaviour
{
    // Public properties for coins and UI text
    private int coins = 0;
    public int Coins
    {
        get { return coins; }
        set
        {
            coins = value;
            UpdateCoinUI();
        }
    }
    public TextMeshProUGUI numCoins; 

    // Maximum slots for the inventory
    private int maxSlots = 27;
    public int maxSlotsMoment = 9;
    public GameObject slotPrefab;
    public List<InventorySlot> inventorySlots;

    // GameObjects for inventory display
    public GameObject itemContainer;
    public GameObject inventoryMenu;
    public GameObject itemPikeablePrefab;
    public GameObject menuDesplegableToPass;

    // Reference to the item info panel
    public InventoryItemInfoPanel inventoryItemInfoPanel;

    // Slots for specific item types
    public InventorySlot armorSlot;
    public InventorySlot shieldSlot;
    public InventorySlot weaponSlot;

    private void Start()
    {
        // Initialize inventory slots
        AddSlotsAndCreateInventorySlots();
    }

    public void Update()
    {
        // Ensure the inventory has the maximum allowed slots
        if (inventorySlots.Count < maxSlotsMoment)
        {
            AddSlotsAndCreateInventorySlots();
        }
        // Update the UI to show current coins
        UpdateCoinUI();
    }

    public void UpdateCoinUI()
    {
        // Update the coin display UI
        numCoins.text = Coins.ToString();
    }

    public void AddSlotsAndCreateInventorySlots()
    {
        // Add new inventory slots up to the maximum allowed
        if (maxSlotsMoment <= maxSlots)
        {
            for (int i = inventorySlots.Count; i < maxSlotsMoment; i++)
            {
                GameObject slotInstance = Instantiate(slotPrefab, itemContainer.transform);
                InventorySlot slotComponent = slotInstance.GetComponent<InventorySlot>();
                
                // Set references for the slot
                slotComponent.inventoryItemInfoPanel = inventoryItemInfoPanel;
                slotComponent.slotId = i;
                slotComponent.inventory = inventoryMenu;
                slotComponent.menuDesplegable = menuDesplegableToPass;

                if (slotComponent != null)
                {
                    inventorySlots.Add(slotComponent);  
                }
            }
        }
    }

    public void AddItem(Items itemToAdd)
    {
        // Add an item to the first available empty slot
        int emptySlotIndex = FindEmptySlot();
        if (emptySlotIndex != -1)
        {
            inventorySlots[emptySlotIndex].itemIn = itemToAdd;
        }
        else
        {
            Debug.Log("Inventory full. Cannot add more items.");
        }
    }

    public void RemoveItem(int slotIdToRemove)
    {
        // Remove an item from the specified slot
        if (slotIdToRemove >= 0 && slotIdToRemove < inventorySlots.Count)
        {
            inventorySlots[slotIdToRemove].itemIn = null;
        }
        else
        {
            Debug.Log("Invalid slot ID: " + slotIdToRemove);
        }
    }

    public void DropItem(int slotToDrop)
    {
        // Drop an item from the specified slot
        if (slotToDrop >= 0 && slotToDrop < inventorySlots.Count)
        {
            InventorySlot slot = inventorySlots[slotToDrop];
            Items itemToDrop = slot.itemIn;

            if (itemToDrop != null)
            {
                GameObject itemPickable = Instantiate(itemPikeablePrefab, this.transform.position, Quaternion.identity);
                ItemInWorld itemPickableScript = itemPickable.GetComponent<ItemInWorld>();
                itemPickableScript.itemsPickable = slot.itemIn;
                itemPickableScript.cooldown = 15.0f;
                RemoveItem(slotToDrop);
            }
        }
        else
        {
            Debug.Log("Invalid slot ID: " + slotToDrop);
        }
    }

    public void EquipItem(int slotToEquip)
    {
        // Equip an item from the specified slot
        if (inventorySlots[slotToEquip].itemIn is Armor)
        {
            inventorySlots[slotToEquip].ReciveItemSlot(armorSlot);
        }
        else if (inventorySlots[slotToEquip].itemIn is Weapon)
        {
            inventorySlots[slotToEquip].ReciveItemSlot(weaponSlot);
        }
        else if (inventorySlots[slotToEquip].itemIn is Shield)
        {
            inventorySlots[slotToEquip].ReciveItemSlot(shieldSlot);
        }
    }

    public void UseItem(int slotToUse)
    {
        // Use an item from the specified slot
        RemoveItem(slotToUse);
    }

    public int FindEmptySlot()
    {
        // Find the first empty slot in the inventory
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].itemIn == null)
            {
                return i;
            }
        }
        return -1; // Return -1 if no empty slot is found
    }

    public int FindItem(Items itemToFind)
    {
        // Find the index of a specific item in the inventory
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].itemIn != null && inventorySlots[i].itemIn == itemToFind)
            {
                return i;
            }
        }
        return -1; // Return -1 if the item is not found
    }
}