using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryItemInfoPanel : MonoBehaviour
{
    private Items _itemIn;

    public Items itemIn
    {
        get { return _itemIn; }
        set 
        { 
            _itemIn = value; 
            UpdateItemInfo(); // Automatically update the UI when the item is set
        }
    }
    public TextMeshProUGUI ItemNamePanel;
    public TextMeshProUGUI ItemDescriptionPanel;
    public TextMeshProUGUI ItemValuePanel;
    public TextMeshProUGUI ItemDamagePanel;
    public TextMeshProUGUI ItemEffectPanel;

    public GameObject itemValue;
    public GameObject itemDamage;

    private void UpdateItemInfo()
    {
        // Check if there is an item to display
        if(_itemIn  != null)
        {
            ItemNamePanel.text = _itemIn.itemName;
            ItemDescriptionPanel.text = _itemIn.itemDescription;
            ItemValuePanel.text = _itemIn.itemValue.ToString();
            itemValue.SetActive(true);
            itemDamage.SetActive(true);
            if(_itemIn  is Weapon)
            {
                Weapon weapon = _itemIn as Weapon;
                ItemDamagePanel.text = weapon.damage.ToString();
            }
        }
        else
        {
            ItemNamePanel.text = null;
            ItemDescriptionPanel.text = null;
            ItemValuePanel.text = null;
            ItemDamagePanel.text = null;
            itemValue.SetActive(false);
            itemDamage.SetActive(false);
        }
    }
}
