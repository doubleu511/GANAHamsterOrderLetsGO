using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleDoubleJumpLeg : Module
{
    private float jumpPressedTime = 0f;

    public override void ModuleUpdate()
    {
        JumpInput();
    }

    private void JumpInput()
    {
        if (!GameManager.Player.IsGround && GameManager.Player.JumpCount == 1)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumpPressedTime += Time.deltaTime;
                jumpPressedTime = Mathf.Clamp(jumpPressedTime, 0, 1.2f);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                GameManager.Player.JumpCount++;
                GameManager.Player.Rigid.velocity = new Vector2(GameManager.Player.Rigid.velocity.x, 0);
                GameManager.Player.Rigid.AddForce(new Vector2(0, jumpPressedTime * 5), ForceMode2D.Impulse);
                jumpPressedTime = 0;
            }
        }
    }
}