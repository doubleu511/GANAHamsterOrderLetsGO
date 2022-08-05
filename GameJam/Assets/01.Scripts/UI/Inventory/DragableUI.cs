using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image myImg;
    public Module myModule;
    protected bool isDragging = false;

    public bool bEquipmentInventory = false;

    private void Awake()
    {
        myImg = GetComponent<Image>();
    }

    public void SetModule(Module module)
    {
        if (bEquipmentInventory)
        {
            if (myModule != null)
            {
                myModule.ModuleUnequip();
            }

            if (module != null)
            {
                module.ModuleEquip();
            }
        }

        myModule = module;

        if (module != null)
        {
            myImg.color = Color.white;
            myImg.sprite = module.moduleInfo.moduleIconSpr;
        }
        else
        {
            myImg.color = new Color(1, 1, 1, 0);
            myImg.sprite = null;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!myImg.enabled || myImg.sprite == null)
        {
            return;
        }

        if (!bEquipmentInventory)
        {
            GameManager.Global.CharacterSlotContainer.gameObject.SetActive(true);
        }
        else
        {
            GameManager.Global.PlayerInventoryContainer.gameObject.SetActive(true);
        }

        // Activate Container
        GameManager.Global.DragAndDropContainer.gameObject.SetActive(true);
        // Set Data
        GameManager.Global.DragAndDropContainer.SetModule(myModule);
        GameManager.Global.DragAndDropContainer.fromSlot = this;
        myImg.color = new Color(1, 1, 1, 0);
        myImg.sprite = null;
        myImg.enabled = false;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            GameManager.Global.DragAndDropContainer.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!bEquipmentInventory)
        {
            GameManager.Global.CharacterSlotContainer.gameObject.SetActive(false);
        }
        else
        {
            GameManager.Global.PlayerInventoryContainer.gameObject.SetActive(false);
        }

        SetModule(myModule);
        myImg.enabled = true;
        isDragging = false;

        // Reset Contatiner
        GameManager.Global.DragAndDropContainer.gameObject.SetActive(false);
        GameManager.Global.DragAndDropContainer.SetModule(null);
        GameManager.Global.DragAndDropContainer.fromSlot = null;
    }
}
