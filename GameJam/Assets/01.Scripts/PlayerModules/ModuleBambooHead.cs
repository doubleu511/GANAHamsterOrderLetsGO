using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBambooHead : Module
{
    private float flyTime = 0f;

    public override void ModuleEquip()
    {
        Debug.Log("대나무 장착");
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
            if(Input.GetKey(KeyCode.Space) && flyTime <= 1f)
            {
                flyTime += Time.deltaTime;
                float playerDir = Input.GetAxisRaw("Horizontal");
                GameManager.Player.Rigid.velocity = new Vector2(playerDir * GameManager.Player.PlayerSpeed, 3f);
            }
        }
    }
}
