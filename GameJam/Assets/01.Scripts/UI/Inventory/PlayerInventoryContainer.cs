using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInventoryContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public GameObject inventoryEquipUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Module module = GameManager.Game.DragAndDropContainer.savedModule;

        if (module != null)
        {
            // 여기서 장착 가능한 플레이어 인벤토리를 찾는다.
            InventoryTab targetSlot = GameManager.Player.Inventory.GetRemainInventoryTab();

            if (targetSlot != null)
            {
                inventoryEquipUI.SetActive(true);
                inventoryEquipUI.transform.position = targetSlot.playerInventoryTab.transform.position;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inventoryEquipUI.activeSelf)
        {
            inventoryEquipUI.SetActive(false);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemUnequip();
    }

    public void ItemUnequip()
    {
        Module module = GameManager.Game.DragAndDropContainer.savedModule;

        if (module != null)
        {
            // 여기서 장착 가능한 플레이어 인벤토리를 찾는다.
            InventoryTab targetSlot = GameManager.Player.Inventory.GetRemainInventoryTab();

            if (targetSlot != null)
            {
                GameManager.Game.DragAndDropContainer.fromSlot.SetModule(null);
                targetSlot.SetModule(module);
            }

            if (inventoryEquipUI.activeSelf)
            {
                inventoryEquipUI.SetActive(false);
            }
        }
    }
}
