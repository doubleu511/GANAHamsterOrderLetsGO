using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBoosterHead : Module
{
    public Module legModule;

    public override void ModuleEquip()
    {
        legModule.gameObject.SetActive(false);
    }

    public override void ModuleUnequip()
    {
        legModule.gameObject.SetActive(true);
    }

    public override void ModuleUpdate()
    {
        if (!GameManager.Player.CanMove) return;
        JumpInput();

        float playerDir = Input.GetAxisRaw("Horizontal");
        if (playerDir != 0) // 이동중
        {
            GameManager.Player.SpriteFlipX(playerDir > 0);

            // 머리앵그리 다리walk
            GameManager.Player.SetFaceAnim(true);
            GameManager.Player.SetWalkAnim(true);
        }
        else
        {
            GameManager.Player.SetFaceAnim(false);
            GameManager.Player.SetWalkAnim(false);
        }

        Vector2 dir = new Vector2(playerDir * GameManager.Player.PlayerSpeed, GameManager.Player.Rigid.velocity.y);
        GameManager.Player.Rigid.velocity = dir;
    }

    private void JumpInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameManager.Player.Rigid.velocity = new Vector2(GameManager.Player.Rigid.velocity.x, 2);

            // 머리앵그리 다리idle
            GameManager.Player.SetFaceAnim(true);
            GameManager.Player.SetWalkAnim(false);
        }
    }
}
