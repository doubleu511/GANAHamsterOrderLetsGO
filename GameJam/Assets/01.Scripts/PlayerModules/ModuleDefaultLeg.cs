using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleDefaultLeg : Module
{
    // 변수 넣는 용도로 Monobehaviour로 한걸로
    private float jumpPressedTime = 0f;

    private bool isJumpCharging
    {
        get
        {
            return jumpPressedTime > 0;
        }
    }

    public override void ModuleEquip()
    {

    }

    public override void ModuleUnequip()
    {

    }

    public override void ModuleUpdate()
    {
        JumpInput();
        if (!GameManager.Player.IsGround) return;

        // 플레이어 이동 코드가 실행될꺼임
        float chargingSpeedScale = isJumpCharging ? 0.2f : 1f;

        float playerDir = Input.GetAxisRaw("Horizontal");
        if (playerDir != 0)
        {
            GameManager.Player.SpriteFlipX(playerDir > 0);
            GameManager.Player.SetWalkAnim(true);
        }
        else
        {
            GameManager.Player.SetWalkAnim(false);
        }
        Vector2 dir = new Vector2(playerDir * GameManager.Player.PlayerSpeed * chargingSpeedScale, 
                                    GameManager.Player.Rigid.velocity.y);
        GameManager.Player.Rigid.velocity = dir;
    }

    private void JumpInput()
    {
        if (GameManager.Player.JumpCount == 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumpPressedTime += Time.deltaTime;
                jumpPressedTime = Mathf.Clamp(jumpPressedTime, 0, 1.2f);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                GameManager.Player.JumpCount++;
                GameManager.Player.Rigid.AddForce(new Vector2(0, jumpPressedTime * 10), ForceMode2D.Impulse);
                jumpPressedTime = 0;
            }
        }
    }
}