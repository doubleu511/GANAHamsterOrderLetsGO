using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleDoubleJumpLeg : Module
{
    public Sprite leftFeetSpr;
    public Sprite rightFeetSpr;

    public override void ModuleEquip()
    {
        GameManager.Player.JumpMaxCount = 2;
        GameManager.Player.Feet.SetFeet(leftFeetSpr, rightFeetSpr);
    }

    public override void ModuleUnequip()
    {
        GameManager.Player.JumpMaxCount = 1;
        GameManager.Player.Feet.ResetFeet();
    }

    public override void ModuleUpdate()
    {
        JumpInput();
    }

    private void JumpInput()
    {
        if (!GameManager.Player.IsGround && GameManager.Player.JumpCount == 1)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                float playerDir = Input.GetAxisRaw("Horizontal");

                GameManager.Player.JumpCount++;
                GameManager.Player.Rigid.velocity = new Vector2(playerDir * GameManager.Player.PlayerSpeed, 0);
                GameManager.Player.Rigid.AddForce(new Vector2(0, 5.7f), ForceMode2D.Impulse);

                Global.Sound.Play("SFX/sfx_Jump", Define.Sound.Effect);
            }
        }
    }
}