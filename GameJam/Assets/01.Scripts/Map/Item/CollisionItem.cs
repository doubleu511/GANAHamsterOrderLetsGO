using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class CollisionItem : MonoBehaviour
{
    protected Collider2D coll;

    protected virtual void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        Vector2 pos = transform.position;
        transform.DOMove(new Vector2(pos.x, pos.y + .1f), 1).SetLoops(-1, LoopType.Yoyo);
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