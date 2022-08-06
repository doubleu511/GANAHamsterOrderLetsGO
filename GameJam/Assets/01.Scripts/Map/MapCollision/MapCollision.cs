using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCollision : MonoBehaviour
{
    protected Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnEnter();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnExit();
        }
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnExit()
    {

    }
}
