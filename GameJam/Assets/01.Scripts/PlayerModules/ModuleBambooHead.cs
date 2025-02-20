using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBambooHead : Module
{
    private Animator propellerAnimator;

    public float flyTime = 0f;
    private float flyMaxTime = 0.4f;
    private float playerDir;

    private void Awake()
    {
        propellerAnimator = GetComponentInChildren<Animator>();
    }

    public override void ModuleEquip()
    {

    }

    public override void ModuleUnequip()
    {

    }

    public override void ModuleUpdate()
    {
        if (!GameManager.Player.IsGround)
        {
            Fly();
        }

        if(GameManager.Player.IsGround && GameManager.Player.IsFalling)
        {
            flyTime = 0f;
            propellerAnimator.SetBool("isMove", false);
        }
    }

    private void Fly()
    {
        if(GameManager.Player.JumpCount == GameManager.Player.JumpMaxCount)
        {
            if(Input.GetKey(KeyCode.Space) && flyTime < flyMaxTime)
            {
                if(flyTime <= 0) // 처음 날때 방향고정
                {
                    Global.Sound.Play("SFX/sfx_Propeller", Define.Sound.Effect);
                    playerDir = Input.GetAxisRaw("Horizontal");
                    GameManager.Player.SpriteFlipX(playerDir > 0);
                    propellerAnimator.SetBool("isMove", true);
                }

                flyTime += Time.deltaTime;
                GameManager.Player.Rigid.velocity = new Vector2(playerDir * GameManager.Player.PlayerSpeed, 3f);
            }

            if(Input.GetKeyUp(KeyCode.Space))
            {
                flyTime = flyMaxTime;
            }
        }
    }
}
