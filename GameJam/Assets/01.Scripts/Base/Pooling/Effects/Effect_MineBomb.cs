using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_MineBomb : Effect
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Global.Sound.Play("SFX/sfx_Explosion_Mine", Define.Sound.Effect);
    }
}
