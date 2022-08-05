using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSlotContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler 
{
    public GameObject equipableUI;

    public DragableUI headSlot;
    public DragableUI armSlot;
    public DragableUI legSlot;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Module module = GameManager.Game.DragAndDropContainer.savedModule;

        if (module != null)
        {
            DragableUI targetSlot = null;

            switch(module.moduleInfo.moduleSlot)
            {
                case Define.ItemSlot.HEAD:
                    targetSlot = headSlot;
                    break;
                case Define.ItemSlot.ARM:
                    targetSlot = armSlot;
                    break;
                case Define.ItemSlot.LEG:
                    targetSlot = legSlot;
                    break;
            }

            equipableUI.SetActive(true);
            equipableUI.transform.position = targetSlot.transform.position;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(equipableUI.activeSelf)
        {
            equipableUI.SetActive(false);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Module module = GameManager.Game.DragAndDropContainer.savedModule;

        if (module != null)
        {
            DragableUI targetSlot = null;

            switch (module.moduleInfo.moduleSlot)
            {
                case Define.ItemSlot.HEAD:
                    targetSlot = headSlot;
                    break;
                case Define.ItemSlot.ARM:
                    targetSlot = armSlot;
                    break;
                case Define.ItemSlot.LEG:
                    targetSlot = legSlot;
                    break;
            }

            if (targetSlot.myModule != null)
            {
                // ½º¿Ò ÀåÂø
                Module tempModule = targetSlot.myModule;
                targetSlot.SetModule(module);
                GameManager.Game.DragAndDropContainer.fromSlot.SetModule(tempModule);
            }
            else
            {
                // ÀåÂø!
                GameManager.Game.DragAndDropContainer.fromSlot.SetModule(null);
                targetSlot.SetModule(module);
            }

            if (equipableUI.activeSelf)
            {
                equipableUI.SetActive(false);
            }
        }
    }
}