using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInventory Inventory;
    public PlayerFoot Feet;
    public Rigidbody2D Rigid;
    public float PlayerSpeed = 3;

    Module[] modules;
    PlayerAnimation PlayerAnim;
    bool firstFallSoundPlayed = false;
    bool fallFlag = false;
    bool isLand = false;
    float fallTime = 0f;
    

    public bool IsFalling
    {
        get
        {
            return Rigid.velocity.y <= 0;
        }
    }

    public bool CanMove { get; set; } = true;

    public bool IsGround { get; set; } = false;

    public Action OnGroundCollision { get; set; }
    public int JumpCount { get; set; } = 0;
    public int JumpMaxCount { get; set; } = 1;

    public SpriteRenderer head;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        PlayerAnim = GetComponent<PlayerAnimation>();
        modules = GetComponentsInChildren<Module>(true);
    }

    private void Start()
    {
        foreach (Module module in modules) // 모듈들 장착
        {
            if (module.gameObject.activeSelf)
            {
                module.ModuleEquip();
            }
        }
    }

    public void Update()
    {
        foreach (Module module in modules)
        {
            if (module.gameObject.activeSelf)
            {
                module.ModuleUpdate();
            }
        }

        

        IsGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.4f), 0.3f, 1 << LayerMask.NameToLayer("Ground"));


        if (!IsGround) // 공중에있음
        {
            isLand = false;
            SetFaceAnim(false);
            SetHeadAnim(true);

            if (!IsFalling) // 상승중
            {
                SetJumpAnim(true);
                SetFallAnim(false);
                fallTime = 0;
            }
            else // 하강중
            {
                fallTime += Time.deltaTime;

                SetJumpAnim(false);
                SetFallAnim(true);

                if (fallTime >= 1.5f && !firstFallSoundPlayed)
                {
                    Global.Sound.PlayNotOne("SFX/sfx_Falling", NotOneShot.FirstFalling);
                    firstFallSoundPlayed = true;
                }
                if (fallTime >= 2f && !fallFlag)
                {
                    SubtitlePanel subtitle = FindObjectOfType<SubtitlePanel>();
                    subtitle.ShowSubtitle(subtitle.onFallDown);
                    fallFlag = true;
                }
            }
        }
        else // 땅에있음
        {
            fallTime = 0f;
            SetHeadAnim(false);
        }

        if (IsGround && IsFalling)
        {
            JumpCount = 0;
            OnGroundCollision?.Invoke();
            SetJumpAnim(false);
            SetFallAnim(false);

            if (firstFallSoundPlayed)
            {
                Debug.Log("도착");
                Global.Sound.StopNotOne("SFX/sfx_Falling", NotOneShot.FirstFalling);
                Global.Sound.Play("SFX/sfx_FallGround", Define.Sound.Effect, 1f);
                firstFallSoundPlayed = false;
                fallFlag = false;
                isLand = true;
            }
            else if(!isLand)
            {
                isLand = true;
                Global.Sound.Play("SFX/sfx_FallGround", Define.Sound.Effect, 0.2f);
            }
        }

        if (!CanMove)
        {
            SetFaceAnim(false);
            SetHeadAnim(true);
            SetWalkAnim(false);
            SetFallAnim(false);
            SetJumpAnim(false);
            return;
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

    public void ResetJumpCharge()
    {
        foreach (Module module in modules) // 모듈들 장착
        {
            if (module is IJumpReset)
            {
                (module as IJumpReset).JumpReset();
            }
        }
    }

    public void SetHeadAnim(bool isIdle)
    {
        PlayerAnim.SetIsIdleHead(isIdle);
    }

    public void SetFaceAnim(bool isAngry)
    {
        // if (!IsGround) isAngry = false; // 공중일땐 화안남
        PlayerAnim.SetIsAngry(isAngry);
    }

    public void SetWalkAnim(bool isWalk)
    {
        PlayerAnim.SetIsWalk(isWalk);
    }

    public void SetJumpAnim(bool isJump)
    {
        PlayerAnim.SetIsJump(isJump);
    }

    public void SetFallAnim(bool isFall)
    {
        PlayerAnim.SetIsFall(isFall);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(0, -0.4f), 0.3f);
    }
}
