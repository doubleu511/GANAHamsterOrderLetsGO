using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoreblockUI : MonoBehaviour
{
    private RectTransform blockRect;
    public RectTransform lorePanel;
    public GameObject tipPanel;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI slotName;
    public TextMeshProUGUI itemLore;

    private readonly string SLOT_PREFIX = "Âø¿ë ºÎÀ§ : ";

    private void Awake()
    {
        blockRect = GetComponent<RectTransform>();
    }

    public void SetLore(ModuleInfoSO moduleInfo, bool isLeft)
    {
        Vector2 pivot = new Vector2(0, lorePanel.pivot.y);
        pivot.x = isLeft ? 1 : 0;

        lorePanel.pivot = pivot;
        itemName.text = moduleInfo.moduleName;
        tipPanel.SetActive(WorkPlace.IsWorkPlaceOpen);

        switch (moduleInfo.moduleSlot)
        {
            case Define.ItemSlot.HEAD:
                slotName.text = SLOT_PREFIX + "¸Ó¸®";
                break;
            case Define.ItemSlot.ARM:
                slotName.text = SLOT_PREFIX + "ÆÈ";
                break;
            case Define.ItemSlot.LEG:
                slotName.text = SLOT_PREFIX + "´Ù¸®";
                break;
        }
        
        itemLore.text = moduleInfo.moduleLore;
        transform.SetAsLastSibling();
        StartCoroutine(ShowLore());
    }

    public void Disappear()
    {
        blockRect.DOSizeDelta(new Vector2(0, blockRect.sizeDelta.y), 0.5f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private IEnumerator ShowLore()
    {
        yield return null;
        yield return null;
        lorePanel.anchoredPosition = Vector2.zero;
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)lorePanel.transform);
        blockRect.DOSizeDelta(new Vector2(lorePanel.sizeDelta.x * 2, blockRect.sizeDelta.y), 0.25f);
    }
}
