using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class CollisionItem : MonoBehaviour
{
    protected Transform itemTrm;
    protected Collider2D coll;

    protected virtual void Awake()
    {
        itemTrm = transform.Find("ItemSpr");
        coll = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        Vector2 pos = transform.position;
        itemTrm.DOMove(new Vector2(pos.x, pos.y + .2f), 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            coll.enabled = false;
            OnEnter();
        }
    }

    public abstract void OnEnter();
}
