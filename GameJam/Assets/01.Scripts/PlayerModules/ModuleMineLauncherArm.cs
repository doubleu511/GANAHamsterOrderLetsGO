using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModuleMineLauncherArm : ModuleDefaultArm
{
    public GameObject MinePrefab;
    public GameObject MineBombVfx;
    private int mineCount = 0;
 

    private void Start()
    {
        Global.Pool.CreatePool<MineObject>(MinePrefab, GameManager.Game.transform);
        Global.Pool.CreatePool<Effect_MineBomb>(MineBombVfx, GameManager.Game.transform,15);
    }
    public override void ArmMoving()
    {
        base.ArmMoving();
        if (Input.GetMouseButtonDown(0) && mineCount < 2 && !EventSystem.current.IsPointerOverGameObject())
        {
            MineObject mineObj = Global.Pool.GetItem<MineObject>();
            mineObj.rigid = mineObj.GetComponent<Rigidbody2D>();

            Vector3 anglePos = arms[mineCount].position;
            //print(mineCount);
            Debug.DrawRay(anglePos, dirs[mineCount].normalized, Color.red, 1);
            RaycastHit2D hit = Physics2D.Raycast(anglePos, dirs[mineCount].normalized, .5f, (1 << LayerMask.NameToLayer("Ground")) + (1 << LayerMask.NameToLayer("Slope")));
            Action OnBombAct = () => { mineCount--; arms[mineCount].GetComponent<SpriteRenderer>().enabled = true; };
            if (hit.collider != null)
            {
                print(hit.collider.name);
                
                mineObj.CallOnHit(hit, OnBombAct);
                StartCoroutine(mineObj.LifeProcess(4, OnBombAct));
            }
            else
            {
                mineObj.transform.position = arms[mineCount].position;
                mineObj.MineMove(dirs[mineCount], OnBombAct);
            }
            arms[mineCount].GetComponent<SpriteRenderer>().enabled = false;
            mineCount++;
            // 지뢰 런처 발사 효과
        }
    }
}
