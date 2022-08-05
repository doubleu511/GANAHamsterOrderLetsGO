using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ModuleDefaultLeg : Module, IJumpReset
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
        if (!GameManager.Player.CanMove)
        {
            return;
        }

        JumpInput();
        if (!GameManager.Player.IsGround) return;
        if (jumpPressedTime > 0) return;

        // 플레이어 이동 코드가 실행될꺼임
        float chargingSpeedScale = isJumpCharging ? 0.2f : 1f;

        float playerDir = Input.GetAxisRaw("Horizontal");
        if (playerDir != 0) // 이동중
        {
            GameManager.Player.SpriteFlipX(playerDir > 0);

            // 머리앵그리 다리walk
            GameManager.Player.SetFaceAnim(true);
            GameManager.Player.SetWalkAnim(true);
        }
        else // 이동안하는중
        {
            if (jumpPressedTime <= 0) // 차징안하는중
            {
                // 머리idle 다리idle
                GameManager.Player.SetFaceAnim(false);
                GameManager.Player.SetWalkAnim(false);
            }
        }
        Vector2 dir = new Vector2(playerDir * GameManager.Player.PlayerSpeed * chargingSpeedScale, 
                                    GameManager.Player.Rigid.velocity.y);
        GameManager.Player.Rigid.velocity = dir;
    }

    private void JumpInput()
    {
        if (GameManager.Player.JumpCount == 0)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Player.Rigid.velocity = Vector2.zero;

                GameManager.Player.head.transform.DOKill();
                GameManager.Player.head.transform.DOScaleY(0.8f, 0.6f);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                jumpPressedTime += Time.deltaTime;
                jumpPressedTime = Mathf.Clamp(jumpPressedTime, 0, 0.6f);

                // 머리앵그리 다리idle
                GameManager.Player.SetFaceAnim(true);
                GameManager.Player.SetWalkAnim(false);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                GameManager.Player.JumpCount++;
                jumpPressedTime = Mathf.Clamp(jumpPressedTime, 0f, 0.6f);
                GameManager.Player.Rigid.AddForce(new Vector2(0, jumpPressedTime * 8.3f + 3.2f), ForceMode2D.Impulse);

                GameManager.Player.head.transform.DOKill();
                GameManager.Player.head.transform.DOScaleY(1f, 0.6f).SetEase(Ease.OutBack);

                jumpPressedTime = 0;
            }
        }
    }

    public void JumpReset()
    {
        if (jumpPressedTime != 0)
        {
            jumpPressedTime = 0f;
            GameManager.Player.head.transform.DOKill();
            GameManager.Player.head.transform.DOScaleY(1f, 0.6f).SetEase(Ease.OutBack);
            GameManager.Player.JumpCount++;
        }
    }

}