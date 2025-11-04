using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ModuleDefaultLeg : Module, IJumpReset
{
    // ���� �ִ� �뵵�� Monobehaviour�� �Ѱɷ�
    private float jumpPressedTime = 0f;
    private bool firstFall = false;

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

        float playerDir = Input.GetAxisRaw("Horizontal");
        if (playerDir != 0) // �̵���
        {
            GameManager.Player.SpriteFlipX(playerDir > 0);
        }

        if (!GameManager.Player.IsGround)
        {
            if (firstFall) // ���߿��� �� �����������
            {
                // ����
                GameManager.Player.head.transform.DOKill();
                GameManager.Player.head.transform.DOScaleY(1f, 0.2f);
                jumpPressedTime = 0f;
                firstFall = false;
            }

            return;
        }
        else
        {
            firstFall = true;
        }

        JumpInput();

        if (jumpPressedTime > 0) return;

        if (playerDir != 0) // �̵���
        {
            // �ٸ�walk
            GameManager.Player.SetWalkAnim(true);
        }
        else // �̵����ϴ���
        {
            if (jumpPressedTime <= 0) // ��¡���ϴ���
            {
                // �Ӹ�idle �ٸ�idle
                GameManager.Player.SetWalkAnim(false);
            }
        }

        GameManager.Player.SetFaceAnimAngry(false);
        Vector2 dir = new Vector2(playerDir * GameManager.Player.PlayerSpeed, GameManager.Player.Rigid.linearVelocity.y);
        GameManager.Player.Rigid.linearVelocity = dir;
    }

    private void JumpInput()
    {
        if (GameManager.Player.JumpCount == 0)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Player.Rigid.linearVelocity = Vector2.zero;

                GameManager.Player.head.transform.DOKill();
                GameManager.Player.head.transform.DOScaleY(0.8f, 0.6f);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                jumpPressedTime += Time.deltaTime;
                jumpPressedTime = Mathf.Clamp(jumpPressedTime, 0, 0.6f);

                // �Ӹ��ޱ׸� �ٸ�idle
                GameManager.Player.SetFaceAnimAngry(true);
                GameManager.Player.SetWalkAnim(false);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                GameManager.Player.JumpCount++;
                jumpPressedTime = Mathf.Clamp(jumpPressedTime, 0f, 0.6f);
                GameManager.Player.Rigid.AddForce(new Vector2(0, jumpPressedTime * 8.3f + 3.2f), ForceMode2D.Impulse);

                GameManager.Player.head.transform.DOKill();
                GameManager.Player.head.transform.DOScaleY(1f, 0.6f).SetEase(Ease.OutBack);

                Global.Sound.Play("SFX/sfx_Jump", Define.Sound.Effect);

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