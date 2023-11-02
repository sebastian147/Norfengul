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
    public int Coins = 0;
    public TextMeshProUGUI numCoins; 

    private int maxSlots = 27;
    public int maxSlotsMoment = 9;
    public GameObject slotPrefab;
    public List<InventorySlot> inventorySlots;
    
    public GameObject itemContainer;
    public GameObject inventoryMenu;
    public GameObject itemPikeablePrefab;
    public GameObject menuDesplegableToPass;

    public InventoryItemInfoPanel inventoryItemInfoPanel;

    public InventorySlot armorSlot;
    public InventorySlot shieldSlot;
    public InventorySlot weaponSlot;


    private void Start ()
    {
        AddSlotsAndCreateInventorySlots();
    }

    public void Update ()
    {
        if(inventorySlots.Count < maxSlotsMoment)
        {
            AddSlotsAndCreateInventorySlots();
        }
        UpdateCoinUI();
    }

    public void UpdateCoinUI ()
    {
        numCoins.text = Coins.ToString();
    }

    public void AddSlotsAndCreateInventorySlots ()
    {
        if(maxSlotsMoment <= maxSlots)
        {
            for (int i = inventorySlots.Count; i < maxSlotsMoment; i++)
            {
                GameObject slotInstance = Instantiate(slotPrefab, itemContainer.transform);
                InventorySlot slotComponent = slotInstance.GetComponent<InventorySlot>();
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

    public void AddItem (Items ItemToAdd)
    {
        int emptySlotIndex = FindEmptySlot();
        if (emptySlotIndex != -1)
        {
            inventorySlots[emptySlotIndex].itemIn = ItemToAdd;
        }
        else
        {
            Debug.Log("Inventario lleno. No se puede agregar más items.");
        }
    }

    public void RemoveItem(int SlotIdToRemove)
    {
        if (SlotIdToRemove >= 0 && SlotIdToRemove < inventorySlots.Count)
        {
            inventorySlots[SlotIdToRemove].itemIn = null;
        }
        else
        {
            Debug.Log("ID de slot no válido: " + SlotIdToRemove);
        }
    }

    public void DropItem(int SlotToDrop)
    {
        if (SlotToDrop >= 0 && SlotToDrop < inventorySlots.Count)
        {
            InventorySlot slot = inventorySlots[SlotToDrop];
            Items itemToDrop = slot.itemIn;

            if (itemToDrop != null)
            {
                GameObject itemPickable = Instantiate(itemPikeablePrefab, this.transform.position, Quaternion.identity);
                ItemInWorld itemPickableScript = itemPickable.GetComponent<ItemInWorld>();
                itemPickableScript.itemsPickable = slot.itemIn;
                itemPickableScript.cooldown = 15.0f;
                RemoveItem(SlotToDrop);
            }
        }
        else
        {
            Debug.Log("ID de slot no válido: " + SlotToDrop);
        }
    }

    public void EquipItem(int SlotToEquip)
    {
        if(inventorySlots[SlotToEquip].itemIn is Armor)
        {
            inventorySlots[SlotToEquip].ReciveItemSlot(armorSlot);
        }
        else
        {
            if(inventorySlots[SlotToEquip].itemIn is Weapon)
            {
                inventorySlots[SlotToEquip].ReciveItemSlot(weaponSlot);
            }
            else
            {
                if(inventorySlots[SlotToEquip].itemIn is Shield)
                {
                    inventorySlots[SlotToEquip].ReciveItemSlot(shieldSlot);
                }
            }
        }
    }

    public void UseItem(int SlotToUse)
    {
        RemoveItem(SlotToUse);
    }


    public int FindEmptySlot ()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].itemIn == null)
            {
                return i;
            }
        }
        return -1;
    }
}
