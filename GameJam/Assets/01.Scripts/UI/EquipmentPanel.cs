using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class EquipmentPanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public CanvasGroup characterPanel;

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
        Global.Sound.Play("SFX/sfx_ModuleChange2", Define.Sound.Effect, 2);
        Global.UI.UIFade(canvasGroup, true);
        GameManager.Player.Inventory.OpenOrClose(true);

        characterPanel.GetComponent<RectTransform>().DOKill();
        characterPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
        characterPanel.GetComponent<RectTransform>().DOSizeDelta(new Vector2(600,425), 0.5f).OnComplete(() =>
        {
            Sequence seq = DOTween.Sequence();
            seq.AppendCallback(() => characterPanel.alpha = 0.5f);
            seq.AppendInterval(0.2f);
            seq.AppendCallback(() => characterPanel.alpha = 0.8f);
            seq.AppendInterval(0.05f);
            seq.AppendCallback(() => characterPanel.alpha = 0.25f);
            seq.AppendInterval(0.05f);
            seq.AppendCallback(() => characterPanel.alpha = 0.7f);
            seq.AppendInterval(0.1f);
            seq.AppendCallback(() => characterPanel.alpha = 0.4f);
            seq.AppendInterval(0.05f);
            seq.AppendCallback(() => characterPanel.alpha = 1f);
        }).SetEase(Ease.Linear);
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
