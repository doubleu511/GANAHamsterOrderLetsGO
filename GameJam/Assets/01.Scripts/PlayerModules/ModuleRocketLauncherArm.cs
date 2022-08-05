using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleRocketLauncherArm : ModuleDefaultArm
{
    public GameObject RocketPrefab;
    private int RocketCount = 0;
    
    private void Start()
    {
        Global.Pool.CreatePool<RocketObject>(RocketPrefab, GameManager.Game.transform);
        GameManager.Player.OnGroundCollision += ()=> { RocketCount = 0; };
    }
    public override void ArmMoving()
    {
        base.ArmMoving();
        if(Input.GetMouseButtonDown(0) && RocketCount == 0)
        {
           
            GameManager.Player.Rigid.velocity = Vector2.zero;
            for (int i = 0; i < arms.Length; i++)
            {
                RocketObject rocketObj = Global.Pool.GetItem<RocketObject>();
                rocketObj.transform.position = arms[i].position;
                rocketObj.RocketMove(dirs[i], () => { rocketObj.gameObject.SetActive(false); });
                GameManager.Player.Rigid.AddForce((dirs[i] * -1) * 5, ForceMode2D.Impulse);
                RocketCount++;
            }
        }
    }
}
