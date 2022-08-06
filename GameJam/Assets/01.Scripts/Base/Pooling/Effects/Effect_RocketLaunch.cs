using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_RocketLaunch : Effect
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Global.Sound.Play("SFX/sfx_Explosion_Rocket", Define.Sound.Effect);
    }
}
