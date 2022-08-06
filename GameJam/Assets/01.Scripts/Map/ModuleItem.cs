using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ModuleItem : CollisionItem
{
    private SpriteRenderer sr;
    public Module moduleItem;

    protected override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        sr.sprite = moduleItem.moduleInfo.moduleIconSpr;
    }

    public override void OnEnter()
    {
        GameManager.Player.Inventory.InventoryAdd(moduleItem);
        Global.Sound.Play("SFX/sfx_GetModule", Define.Sound.Effect);

        transform.DOKill();
        transform.DOMoveY(1, 1).SetRelative();
        sr.DOColor(new Color(1, 1, 1, 0), 1).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
