using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleDefaultArm : Module
{
    public override void ModuleEquip()
    {

    }

    public override void ModuleUpdate()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint( Input.mousePosition);
        pos.z = 0;
        Vector3 playerPos = GameManager.Player.gameObject.transform.position;
        Vector3 dir = pos - playerPos;
        transform.position = playerPos + Vector3.ClampMagnitude(dir , 2.5f);
        var angle = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
