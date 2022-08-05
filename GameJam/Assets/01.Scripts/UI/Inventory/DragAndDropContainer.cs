using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDropContainer : MonoBehaviour
{
    private Image image;
    public DragableUI fromSlot;
    public Module savedModule;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetModule(Module module)
    {
        savedModule = module;
        if (module != null)
        {
            image.sprite = module.moduleInfo.moduleIconSpr;
        }
    }
}
