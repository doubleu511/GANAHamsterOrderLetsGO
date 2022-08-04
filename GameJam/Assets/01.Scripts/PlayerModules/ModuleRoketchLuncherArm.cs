using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleRoketchLuncherArm : ModuleDefaultArm
{
    public GameObject RocketPrefab;
    private void Start()
    {
        Global.Pool.CreatePool<RocketObject>(RocketPrefab, GameManager.Global.transform);        
    }
    public override void ArmMoving()
    {
        base.ArmMoving();
        if(Input.GetMouseButtonDown(0))
        {
            RocketObject rocketObj = Global.Pool.GetItem<RocketObject>();
            rocketObj.transform.position = transform.position;
            rocketObj.RocketMove(dir, ()=> rocketObj.gameObject.SetActive(false));
        }
    }
}
