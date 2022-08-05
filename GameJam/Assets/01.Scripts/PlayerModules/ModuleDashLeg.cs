using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleDashLeg : Module
{
    private int dashCount = 0;
    private void Start()
    {
        GameManager.Player.OnGroundCollision += () => { dashCount = 0; };
    }

    public override void ModuleEquip()
    {

    }

    public override void ModuleUpdate()
    {
        if(Input.GetKeyDown(KeyCode.F) && dashCount == 0)
        {
            
            GameManager.Player.Rigid.AddForce(new Vector2(Mathf.Sign(GameManager.Player.transform.localScale.x * -1) *6, 0), ForceMode2D.Impulse);
            dashCount++;
        }
    }

    public override void ModuleUnequip()
    {

    }
}
