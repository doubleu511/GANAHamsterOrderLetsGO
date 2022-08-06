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
    private List<MineObject> mineObjs = new List<MineObject>();

    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            MineObject mineObj = Instantiate(MinePrefab, GameManager.Game.transform).GetComponent<MineObject>();
            mineObj.rigid = mineObj.GetComponent<Rigidbody2D>();
            mineObj.isLeftMine = i != 0;
            GameManager.Player.OnCollisionWall += () => { mineObj.gameObject.SetActive(false); };
            GameManager.Player.OnGroundCollision += () => { mineObj.gameObject.SetActive(true); };
            mineObjs.Add(mineObj);
        }
        Global.Pool.CreatePool<Effect_MineBomb>(MineBombVfx, GameManager.Game.transform,15);
    }
    public override void ArmMoving()
    {
        base.ArmMoving();
        foreach (var mine in mineObjs)
        {
            if (!mine.isLaunched)
            {
                mine.transform.position = arms[mine.isLeftMine ? 1 : 0].position;
                mine.transform.rotation = arms[mine.isLeftMine ? 1 : 0].rotation;
            }
        }

        if (Input.GetMouseButtonDown(0) && mineCount < 2 && !EventSystem.current.IsPointerOverGameObject())
        {
            MineObject mineObj = null;
            foreach (var mine in mineObjs)
            {
                if(!mine.isLaunched)
                {
                    mineObj = mine;
                    break;
                }
            }
            if(mineObj != null)
            {
                mineObj.isLaunched = true;

                int armNum = mineObj.isLeftMine ? 1 : 0 ;
                Transform arm = arms[armNum];
                arm.GetComponent<SpriteRenderer>().enabled = false;
                Vector3 anglePos = arm.position;
                //print(mineCount);
                Debug.DrawRay(anglePos, dirs[armNum].normalized, Color.red, 1);
                RaycastHit2D hit = Physics2D.Raycast(anglePos, dirs[armNum].normalized, .5f, (1 << LayerMask.NameToLayer("Ground")) + (1 << LayerMask.NameToLayer("Slope")));
                Action OnBombAct = () => { mineCount--; arm.GetComponent<SpriteRenderer>().enabled = true; };
                if (hit.collider != null)
                {
                    print(hit.collider.name);

                    mineObj.CallOnHit(hit, OnBombAct);
                    StartCoroutine(mineObj.LifeProcess(4, OnBombAct));
                }
                else
                {
                    mineObj.transform.position = arm.position;
                    mineObj.MineMove(dirs[armNum], OnBombAct);
                }
                mineCount++;
                // 지뢰 런처 발사 효과
            }

        }
    }
}