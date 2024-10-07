using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuDesplegable : MonoBehaviour
{
    private int _slotIdRequired;
    public int slotIdRequired
    {
        get { return _slotIdRequired; }
        set 
        { 
            _slotIdRequired = value; 
            UpdateButtonInteractivity(); 
        }
    }
    public InventoryManager InventoryManager { get; set; }

    public GameObject useItemButton;
    public GameObject equipItemButton;
    public GameObject dropItemButton;

    private Button _useItemButtonComponent;
    private Button _equipItemButtonComponent;
    private Button _dropItemButtonComponent;

    private void Start()
    {
        // Initialize button components for later use
        _useItemButtonComponent = useItemButton.GetComponent<Button>();
        _equipItemButtonComponent = equipItemButton.GetComponent<Button>();
        _dropItemButtonComponent = dropItemButton.GetComponent<Button>();

        // Assign listeners to buttons only once in Start to avoid multiple subscriptions
        _useItemButtonComponent.onClick.AddListener(() => InventoryManager.UseItem(_slotIdRequired));
        _equipItemButtonComponent.onClick.AddListener(() => InventoryManager.EquipItem(_slotIdRequired));
        _dropItemButtonComponent.onClick.AddListener(() => InventoryManager.DropItem(_slotIdRequired));
    }

    private void UpdateButtonInteractivity()
    {
        // Check item type and enable/disable equip button accordingly
        if(InventoryManager.inventorySlots[_slotIdRequired].itemIn is Armor 
           || InventoryManager.inventorySlots[_slotIdRequired].itemIn is Weapon 
           || InventoryManager.inventorySlots[_slotIdRequired].itemIn is Shield)
        {
            _equipItemButtonComponent.interactable = true;
        }
        else
        {
            _equipItemButtonComponent.interactable = false;
        }
    }

}
