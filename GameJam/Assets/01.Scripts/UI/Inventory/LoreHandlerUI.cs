using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoreHandlerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isLeft = true;
    private Transform appearTrm;

    private ModuleInfoSO myInfo;
    private LoreblockUI block;

    private void Awake()
    {
        appearTrm = transform.Find("AppearTrm");
    }

    public void SetInfo(ModuleInfoSO info)
    {
        myInfo = info;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       if(myInfo != null)
        {
            block = Global.Pool.GetItem<LoreblockUI>();
            block.transform.position = appearTrm.position;
            block.SetLore(myInfo, isLeft);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(block != null)
        {
            block.Disappear();
            block = null;
        }
    }
}
