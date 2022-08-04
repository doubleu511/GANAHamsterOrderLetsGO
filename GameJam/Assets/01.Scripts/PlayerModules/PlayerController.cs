using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D Rigid;
    public float PlayerSpeed = 3;

    Module[] modules;
    PlayerAnimation PlayerAnim;

    public bool IsFalling
    {
        get
        {
            return Rigid.velocity.y < 0;
        }
    }

    public bool IsGround { get; set; } = false;

    public Action OnGroundCollision { get; set; }
    public int JumpCount { get; set; } = 0;
    public int JumpMaxCount { get; set; } = 1;

    private SpriteRenderer[] playerSprites;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        PlayerAnim = GetComponent<PlayerAnimation>();
        modules = GetComponentsInChildren<Module>(true);
        playerSprites = GetComponentsInChildren<SpriteRenderer>(true);
    }

    private void Start()
    {
        foreach (Module module in modules) // 모듈들 장착
        {
            module.ModuleEquip();
        }
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

        if(!IsGround)
        {
            SetWalkAnim(false);
        }

        if(IsGround && IsFalling)
        {
            JumpCount = 0;
            OnGroundCollision?.Invoke();
        }
    }

    public void SpriteFlipX(bool value)
    {
        float xFlip = value == true ? -1 : 1;
        transform.localScale = new Vector3(xFlip,
            transform.localScale.y, transform.localScale.z);

        //for (int i = 0; i < playerSprites.Length; i++)
        //{
        //    playerSprites[i].flipX = value;
        //}
    }

    public void SetWalkAnim(bool isWalk)
    {
        if (!IsGround) isWalk = false; // 점프하면 Idle
        PlayerAnim.SetIsMove(isWalk);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(0, -0.4f), 0.3f);
    }
}
