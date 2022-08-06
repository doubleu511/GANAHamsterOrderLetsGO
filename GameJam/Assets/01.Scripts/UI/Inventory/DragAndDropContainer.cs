using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDropContainer : MonoBehaviour
{
    public Image image;
    public DragableUI fromSlot;
    public Module savedModule;

    public void SetModule(Module module)
    {
        savedModule = module;
        if (module != null)
        {
            image.sprite = module.moduleInfo.moduleIconSpr;
        }
    }

    public void SetFromSlot(DragableUI ui)
    {
        fromSlot = ui;

        if (ui != null)
        {
            image.rectTransform.sizeDelta = ui.GetComponent<RectTransform>().sizeDelta;
        }
    }
}
