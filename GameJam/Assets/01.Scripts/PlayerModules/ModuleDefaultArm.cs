using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleDefaultArm : Module
{
    public override void ModuleUpdate()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint( Input.mousePosition);
        pos.z = 0;
        Vector3 playerPos = GameManager.Player.gameObject.transform.position;

        transform.position = playerPos + Vector3.ClampMagnitude((pos - playerPos) , 10);

    }
}
