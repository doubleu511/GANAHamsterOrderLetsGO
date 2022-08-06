using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MapCollision
{
    public override void OnEnter()
    {
        if (GameManager.Player.IsGround || ModuleBoosterHead.isBooster)
        {
            coll.sharedMaterial = GameManager.Game.staticPMat;
        }
        else
        {
            Global.Sound.Play("SFX/sfx_FallGround", Define.Sound.Effect);
            GameManager.Player.CanAction = false;
        }
    }

    public override void OnExit()
    {
        coll.sharedMaterial = GameManager.Game.bouncePMat;
    }
}
