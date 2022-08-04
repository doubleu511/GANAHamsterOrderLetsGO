using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleGoggleHead : Module
{
    private float goggleSight = 8f;

    public override void ModuleEquip()
    {
        Debug.Log("°í±Û ÀåÂø");
        CameraMove.ZoomCam(goggleSight, 2f);
    }

    public override void ModuleUpdate()
    {

    }
}
