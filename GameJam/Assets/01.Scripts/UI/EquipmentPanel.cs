using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquipmentPanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public Button cancelButton;

    public TextMeshProUGUI[] abilityLores = new TextMeshProUGUI[(int)Define.ItemSlot.MAXCOUNT];

    private readonly string NONE = "없음!";

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        LoreInit();

        cancelButton.onClick.AddListener(() =>
        {
            Global.Sound.Play("SFX/sfx_PopupClose", Define.Sound.Effect);
            Global.UI.UIFade(canvasGroup, false);
            WorkPlace.IsWorkPlaceOpen = false;
            GameManager.Player.CanMove = true;
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (WorkPlace.IsWorkPlaceOpen)
            {
                Global.Sound.Play("SFX/sfx_PopupClose", Define.Sound.Effect);
                Global.UI.UIFade(canvasGroup, false);
                WorkPlace.IsWorkPlaceOpen = false;
                GameManager.Player.CanMove = true;
            }
        }
    }

    public void PanelOpen()
    {
        Global.Sound.Play("SFX/sfx_ButtonClick", Define.Sound.Effect);
        Global.UI.UIFade(canvasGroup, true);
        GameManager.Player.Inventory.OpenOrClose(true);
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
                prefix = "머리 : ";
                break;
            case Define.ItemSlot.ARM:
                prefix = "팔    : ";
                break;
            case Define.ItemSlot.LEG:
                prefix = "다리 : ";
                break;
        }

        return prefix;
    }
    
}
