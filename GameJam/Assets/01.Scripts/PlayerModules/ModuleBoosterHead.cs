using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBoosterHead : Module
{
    public static bool isBooster = false;

    public Module legModule;

    public override void ModuleEquip()
    {
        legModule.gameObject.SetActive(false);
        isBooster = true;
        CameraMove.ZoomCam(4.75f, 2f);
    }

    public override void ModuleUnequip()
    {
        legModule.gameObject.SetActive(true);
        isBooster = false;
        CameraMove.ZoomCam(3.75f, 2f);
    }

    public override void ModuleUpdate()
    {
        if (!GameManager.Player.CanMove) return;
        JumpInput();

        float playerDir = Input.GetAxisRaw("Horizontal");
        if (playerDir != 0) // �̵���
        {
            GameManager.Player.SpriteFlipX(playerDir > 0);

            // �Ӹ��ޱ׸� �ٸ�walk
            GameManager.Player.SetFaceAnimAngry(true);
            GameManager.Player.SetWalkAnim(true);
        }
        else
        {
            GameManager.Player.SetFaceAnimAngry(false);
            GameManager.Player.SetWalkAnim(false);
        }

        Vector2 dir = new Vector2(playerDir * GameManager.Player.PlayerSpeed, GameManager.Player.Rigid.linearVelocity.y);
        GameManager.Player.Rigid.linearVelocity = dir;
    }

    private void JumpInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameManager.Player.Rigid.linearVelocity = new Vector2(GameManager.Player.Rigid.linearVelocity.x, 3.5f);

            // �Ӹ��ޱ׸� �ٸ�idle
            GameManager.Player.SetFaceAnimAngry(true);
            GameManager.Player.SetWalkAnim(false);
        }
    }
}
