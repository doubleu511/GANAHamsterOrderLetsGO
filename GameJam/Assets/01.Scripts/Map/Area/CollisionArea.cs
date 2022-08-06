using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionArea : MonoBehaviour
{
    protected Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnEnter();
        }
    }

    public abstract void OnEnter();
}
