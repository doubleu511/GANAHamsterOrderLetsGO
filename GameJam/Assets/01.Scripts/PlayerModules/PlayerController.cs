using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D Rigid;
    public float PlayerSpeed = 3;

    Module[] modules;

    public bool IsFalling
    {
        get
        {
            return Rigid.velocity.y < 0;
        }
    }

    public bool IsGround { get; set; } = false;
    public int JumpCount { get; set; } = 0;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        modules = GetComponentsInChildren<Module>(true);
    }

    public void Update()
    {
        foreach (Module item in modules)
        {
            if (item.gameObject.activeSelf)
            {
                item.ModuleUpdate();
            }
        }

        IsGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.4f), 0.3f, 1 << LayerMask.NameToLayer("Ground"));

        if(IsGround && IsFalling)
        {
            JumpCount = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(0, -0.4f), 0.3f);
    }
}
