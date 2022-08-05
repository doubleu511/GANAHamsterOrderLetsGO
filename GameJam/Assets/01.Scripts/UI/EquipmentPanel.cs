using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipmentPanel : MonoBehaviour
{
    private readonly string NONE = "����!";

    public TextMeshProUGUI[] abilityLores = new TextMeshProUGUI[(int)Define.ItemSlot.MAXCOUNT];

    private void Start()
    {
        LoreInit();
    }

    private void LoreInit()
    {
        for(int i =0; i<(int)Define.ItemSlot.MAXCOUNT;i++)
        {
            abilityLores[i].text = GetPrefix((Define.ItemSlot)i) + NONE;
        }
    }

    public void SetLore(ModuleInfoSO info, string text)
    {
        if(text == null)
        {
            text = NONE;
        }

        abilityLores[(int)info.moduleSlot].text = GetPrefix(info.moduleSlot) + text;
        LoreHandlerUI handler = abilityLores[(int)info.moduleSlot].GetComponentInChildren<LoreHandlerUI>();

        handler.SetInfo(text.Equals(NONE) ? null : info);
    }

    private string GetPrefix(Define.ItemSlot slotType)
    {
        string prefix = "";
        switch (slotType)
        {
            case Define.ItemSlot.HEAD:
                prefix = "�Ӹ� : ";
                break;
            case Define.ItemSlot.ARM:
                prefix = "��    : ";
                break;
            case Define.ItemSlot.LEG:
                prefix = "�ٸ� : ";
                break;
        }

        return prefix;
    }
    
}
