using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleDefaultArm : Module
{
    protected Vector3 dir;
    public override void ModuleEquip()
    {

    }

    public override void ModuleUpdate()
    {
        ArmMoving();
    }

    public virtual void ArmMoving()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        Vector3 playerPos = GameManager.Player.gameObject.transform.position;
        dir = pos - playerPos;
        transform.position = playerPos + Vector3.ClampMagnitude(dir, 1.2f);
        var angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public override void ModuleUnequip()
    {

    }
}
