using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ModuleItem : MonoBehaviour
{
    private SpriteRenderer sr;
    private Collider2D coll;

    public Module moduleItem;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    private void Start()
    {
        sr.sprite = moduleItem.moduleInfo.moduleIconSpr;

        Vector2 pos = transform.position;
        transform.DOMove(new Vector2(pos.x, pos.y + .1f), 1).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            coll.enabled = false;

            GameManager.Player.Inventory.InventoryAdd(moduleItem);

            transform.DOKill();
            transform.DOMoveY(1, 1).SetRelative();
            sr.DOColor(new Color(1, 1, 1, 0), 1).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}
