using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleGoggleHead : Module
{
    private float goggleSight = 8f;

    public override void ModuleEquip()
    {
        Debug.Log("고글 장착");
        CameraMove.ZoomCam(goggleSight, 2f);
    }

    public override void ModuleUnequip()
    {
        Debug.Log("고글 장착 해제");
        CameraMove.ZoomCam(3.75f, 2f);
    }

    public override void ModuleUpdate()
    {

    }
}
