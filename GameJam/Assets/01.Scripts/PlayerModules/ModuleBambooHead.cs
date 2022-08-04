using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBambooHead : Module
{
    private float flyTime = 0f;
    private float flyMaxTime = 0.4f;
    private float playerDir;

    public override void ModuleEquip()
    {
        Debug.Log("대나무 장착");
    }

    public override void ModuleUnequip()
    {

    }

    public override void ModuleUpdate()
    {
        Fly();

        if(GameManager.Player.IsGround)
        {
            flyTime = 0f;
        }
    }

    private void Fly()
    {
        if(GameManager.Player.JumpCount == GameManager.Player.JumpMaxCount)
        {
            if(Input.GetKey(KeyCode.Space) && flyTime <= flyMaxTime)
            {
                if(flyTime <= 0) // 처음 날때 방향고정
                {
                    playerDir = Input.GetAxisRaw("Horizontal");
                    GameManager.Player.SpriteFlipX(playerDir > 0);
                }

                flyTime += Time.deltaTime;
                GameManager.Player.Rigid.velocity = new Vector2(playerDir * GameManager.Player.PlayerSpeed, 3f);
            }
        }
    }
}
