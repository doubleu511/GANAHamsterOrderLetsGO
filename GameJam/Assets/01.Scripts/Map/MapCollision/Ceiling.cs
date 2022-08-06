using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MapCollision
{
    public override void OnEnter()
    {
        if (!GameManager.Player.IsGround && !ModuleBoosterHead.isBooster)
        {
            Global.Sound.Play("SFX/sfx_FallGround", Define.Sound.Effect);
            GameManager.Player.CanAction = false;
        }
    }
}
