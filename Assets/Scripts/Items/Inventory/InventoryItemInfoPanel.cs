using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryItemInfoPanel : MonoBehaviour
{
    public Items itemIn;

    public TextMeshProUGUI ItemNamePanel;
    public TextMeshProUGUI ItemDescriptionPanel;
    public TextMeshProUGUI ItemValuePanel;
    public TextMeshProUGUI ItemDamagePanel;
    public TextMeshProUGUI ItemEffectPanel;

    public GameObject itemValue;
    public GameObject itemDamage;

    public void Update()
    {
        if(itemIn != null)
        {
            ItemNamePanel.text = itemIn.itemName;
            ItemDescriptionPanel.text = itemIn.itemDescription;
            ItemValuePanel.text = itemIn.itemValue.ToString();
            itemValue.SetActive(true);
            itemDamage.SetActive(true);
            if(itemIn is Weapon)
            {
                Weapon weapon = itemIn as Weapon;
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
