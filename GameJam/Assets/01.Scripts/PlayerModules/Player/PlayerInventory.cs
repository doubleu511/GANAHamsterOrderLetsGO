using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class InventoryTab
{
    public Module myModule;
    public Image playerInventoryImg;

    public void SetModule(Module module)
    {
        myModule = module;

        if (myModule != null)
        {
            playerInventoryImg.sprite = module.moduleInfo.moduleIconSpr;
            playerInventoryImg.gameObject.SetActive(true);
        }
        else
        {
            playerInventoryImg.sprite = null;
            playerInventoryImg.gameObject.SetActive(false);
        }
    }
}

public class PlayerInventory : MonoBehaviour
{
    private bool isOpen = false;
    public Button backpackBtn;
    public RectTransform inventoryPanel;

    public InventoryTab[] inventoryTabs;
    [SerializeField]
    [Range(6, 9)]
    int inventoryCount = 6;

    private void Start()
    {
        backpackBtn.onClick.AddListener(() => OpenOrClose());
        InventoryInit(inventoryCount);
    }

    private void OpenOrClose()
    {
        isOpen = !isOpen;
        inventoryPanel.DOKill();
        CanvasGroup inventoryGroup = inventoryPanel.GetComponent<CanvasGroup>();

        if (isOpen)
        {
            Global.UI.UIFade(inventoryGroup, true);
            inventoryPanel.DOSizeDelta(new Vector2(160, 800), 1);
        }
        else
        {
            inventoryPanel.DOSizeDelta(new Vector2(160, 0), 1).OnComplete(() =>
            {
                Global.UI.UIFade(inventoryGroup, false);
            });
        }
    }

    public void InventoryInit(int count)
    {
        for (int i = 0; i < count; i++)
        {
            inventoryTabs[i].SetModule(null);
        }
    }

    public bool InventoryAdd(Module module)
    {
        for (int i = 0; i < inventoryCount; i++)
        {
            if (inventoryTabs[i].myModule == null)
            {
                inventoryTabs[i].SetModule(module);
                return true;
            }
        }

        return false;
    }
}
