using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    public int slotId;
    public Items itemIn;

    public Image imageItem;
    public Image imagePhantom;

    public GameObject itemToDrag;
    public GameObject inventory;
    public GameObject menuDesplegable;

    public InventoryItemInfoPanel inventoryItemInfoPanel;

    public Canvas canvas;
    public RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    public Vector3 initialPosition;

    public void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = itemToDrag.GetComponent<RectTransform>();
        canvasGroup = itemToDrag.GetComponent<CanvasGroup>();
        initialPosition = rectTransform.localPosition;

        menuDesplegable.SetActive(false);
    }

    public virtual void Update()
    {
        if (itemIn == null)
        {
            imageItem.gameObject.SetActive(false);
            imagePhantom.gameObject.SetActive(false);
        }
        else
        {
            DisplayItemSprite();
        }
    }

    public virtual void ReciveItemSlot (InventorySlot otherSlot)
    {
        if(otherSlot != null)
        {
            Items itemInPass = otherSlot.itemIn;
            otherSlot.PassItemSlot(itemIn);
            if(otherSlot.itemIn == itemIn)
            {
                itemIn = itemInPass;
            }
        }
    }

    public virtual void PassItemSlot(Items itemToRecive)
    {
        itemIn = itemToRecive;
    }

    public void DisplayItemSprite()
    {
        if (itemIn != null && imageItem != null)
        {
            imageItem.sprite = itemIn.itemSprite;
            imageItem.gameObject.SetActive(true);
            imagePhantom.sprite = itemIn.itemSprite;
            imagePhantom.gameObject.SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemIn != null)
        {
            inventoryItemInfoPanel.itemIn = itemIn;
        }
    }

     public void OnPointerExit(PointerEventData eventData)
    {
        inventoryItemInfoPanel.itemIn = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemIn != null)
        {
            menuDesplegable.SetActive(false);
            itemToDrag.transform.SetParent(inventory.transform);
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemToDrag.transform.SetParent(this.gameObject.transform);
        rectTransform.localPosition = initialPosition;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        InventorySlot droppedSlot = eventData.pointerDrag.GetComponent<InventorySlot>();
        ReciveItemSlot(droppedSlot);
        int droppedSlotId = droppedSlot.slotId;
        Debug.Log("Ítem soltado en el slot ID: " + slotId);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (itemIn == null)
        {
            return;
        }

        InventoryMenuDesplegable inventoryMenuDesplegable = menuDesplegable.GetComponent<InventoryMenuDesplegable>();
        if (menuDesplegable.activeSelf)
        {
            menuDesplegable.SetActive(false);
        }
        else
        {
            menuDesplegable.SetActive(true);

            
            Vector3 newPosition = eventData.position;
            newPosition.x += 100;
            newPosition.y += -55;
            menuDesplegable.transform.position = newPosition;

            inventoryMenuDesplegable.slotIdRequired = slotId;
        }
    }

}