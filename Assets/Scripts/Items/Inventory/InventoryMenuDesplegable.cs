using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuDesplegable : MonoBehaviour
{
    public int slotIdRequired;
    public InventoryManager inventoryManager;

    public GameObject useItemButton;
    public GameObject equipItemButton;
    public GameObject dropItemButton;

    private Button useItemButtonComponent;
    private Button equipItemButtonComponent;
    private Button dropItemButtonComponent;

    public void Start()
    {
        useItemButtonComponent = useItemButton.GetComponent<Button>();
        equipItemButtonComponent = equipItemButton.GetComponent<Button>();
        dropItemButtonComponent = dropItemButton.GetComponent<Button>();
    }

    public void Update()
    {
        useItemButtonComponent.onClick.AddListener(() => inventoryManager.UseItem(slotIdRequired));
        equipItemButtonComponent.onClick.AddListener(() => inventoryManager.EquipItem(slotIdRequired));
        dropItemButtonComponent.onClick.AddListener(() => inventoryManager.DropItem(slotIdRequired));

        if(inventoryManager.inventorySlots[slotIdRequired].itemIn is Armor || inventoryManager.inventorySlots[slotIdRequired].itemIn is Weapon || inventoryManager.inventorySlots[slotIdRequired].itemIn is Shield)
        {
            equipItemButtonComponent.interactable = true;
        }
        else
        {
            equipItemButtonComponent.interactable = false;
        }
    }

}
