using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ModuleItem : CollisionItem
{
    private SpriteRenderer sr;
    private SpriteRenderer itemSr;
    public Module moduleItem;

    protected override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
        itemSr = itemTrm.GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        itemSr.sprite = moduleItem.moduleInfo.moduleIconSpr;
    }

    public override void OnEnter()
    {
        GameManager.Player.Inventory.InventoryAdd(moduleItem);
        Global.Sound.Play("SFX/sfx_GetModule", Define.Sound.Effect);

        itemTrm.DOKill();
        itemTrm.DOMoveY(1, 1).SetRelative();
        itemSr.DOColor(new Color(1, 1, 1, 0), 1).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
