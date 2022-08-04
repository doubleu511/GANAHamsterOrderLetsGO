using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleRocketLuncherArm : ModuleDefaultArm
{
    public GameObject RocketPrefab;
    private int RocketCount = 0;
    
    private void Start()
    {
        Global.Pool.CreatePool<RocketObject>(RocketPrefab, GameManager.Global.transform);
        GameManager.Player.OnGroundCollision += ()=> { RocketCount = 0; };
    }
    public override void ArmMoving()
    {
        base.ArmMoving();
        if(Input.GetMouseButtonDown(0) && RocketCount == 0)
        {
            RocketObject rocketObj = Global.Pool.GetItem<RocketObject>();
            rocketObj.transform.position = transform.position;
            rocketObj.RocketMove(dir, ()=> rocketObj.gameObject.SetActive(false));
            GameManager.Player.Rigid.AddForce((dir.normalized * -1) *10, ForceMode2D.Impulse);
            RocketCount++;
        }
    }
}
