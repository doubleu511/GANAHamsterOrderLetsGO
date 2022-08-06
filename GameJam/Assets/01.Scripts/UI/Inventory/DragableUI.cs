using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image myImg;
    private LoreHandlerUI myLoreHandler;
    public Module myModule;
    protected bool isDragging = false;

    public bool bEquipmentInventory = false;
    private bool isEntering = false;

    private void Awake()
    {
        myImg = GetComponent<Image>();
        myLoreHandler = transform.parent.GetComponent<LoreHandlerUI>();
    }

    public void SetModule(Module module)
    {
        if (bEquipmentInventory)
        {
            if (myModule != null)
            {
                //Global.Sound.Play("SFX/sfx_ModuleChange2", Define.Sound.Effect, 2);
                myModule.Unequip();
            }

            if (module != null)
            {
                Global.Sound.Play("SFX/sfx_ModuleChange", Define.Sound.Effect, 1.5f);
                module.Equip();
            }
        }

        myModule = module;
        myLoreHandler.SetInfo(myModule?.moduleInfo);

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
        if (!myImg.enabled || myImg.sprite == null || !WorkPlace.IsWorkPlaceOpen)
        {
            return;
        }

        if (!bEquipmentInventory)
        {
            GameManager.Game.CharacterSlotContainer.gameObject.SetActive(true);
        }
        else
        {
            GameManager.Game.PlayerInventoryContainer.gameObject.SetActive(true);
        }

        // Activate Container
        GameManager.Game.DragAndDropContainer.gameObject.SetActive(true);
        // Set Data
        GameManager.Game.DragAndDropContainer.SetModule(myModule);
        GameManager.Game.DragAndDropContainer.SetFromSlot(this);
        myImg.color = new Color(1, 1, 1, 0);
        myImg.sprite = null;
        myImg.enabled = false;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            GameManager.Game.DragAndDropContainer.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!bEquipmentInventory)
        {
            GameManager.Game.CharacterSlotContainer.gameObject.SetActive(false);
        }
        else
        {
            GameManager.Game.PlayerInventoryContainer.gameObject.SetActive(false);
        }

        if (myModule != null)
        {
            myImg.color = Color.white;
            myImg.sprite = myModule.moduleInfo.moduleIconSpr;
        }
        else
        {
            myImg.color = new Color(1, 1, 1, 0);
            myImg.sprite = null;
        }
        myImg.enabled = true;
        isDragging = false;

        // Reset Contatiner
        GameManager.Game.DragAndDropContainer.gameObject.SetActive(false);
        GameManager.Game.DragAndDropContainer.SetModule(null);
        GameManager.Game.DragAndDropContainer.SetFromSlot(null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isEntering = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isEntering = false;

    }

    private void Update()
    {
        if (!myImg.enabled || myImg.sprite == null || !WorkPlace.IsWorkPlaceOpen)
        {
            return;
        }

        if (isEntering)
        {
            if(Input.GetMouseButtonDown(1))
            {
                // Set Data
                GameManager.Game.DragAndDropContainer.SetModule(myModule);
                GameManager.Game.DragAndDropContainer.SetFromSlot(this);

                if (bEquipmentInventory)
                {
                    GameManager.Game.PlayerInventoryContainer.ItemUnequip();
                }
                else
                {
                    GameManager.Game.CharacterSlotContainer.ItemEquip();
                }
                myLoreHandler.ExitFunc();
                GameManager.Game.DragAndDropContainer.SetModule(null);
                GameManager.Game.DragAndDropContainer.SetFromSlot(null);
            }
        }
    }
}
