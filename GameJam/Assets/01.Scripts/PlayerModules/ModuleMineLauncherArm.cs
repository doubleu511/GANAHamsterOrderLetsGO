using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMineLauncherArm : ModuleDefaultArm
{
    public GameObject MinePrefab;
    private int mineCount = 0;

    private void Start()
    {
        Global.Pool.CreatePool<MineObject>(MinePrefab, GameManager.Game.transform);
    }
    public override void ArmMoving()
    {
        base.ArmMoving();
        if (Input.GetMouseButtonDown(0) && mineCount == 0)
        {
            MineObject mineObj = Global.Pool.GetItem<MineObject>();

            Vector3 playerPos = GameManager.Player.gameObject.transform.position;

            RaycastHit2D hit = Physics2D.Raycast(playerPos, dir.normalized, Vector3.Distance(playerPos, transform.position), LayerMask.GetMask("Ground") + LayerMask.GetMask("Slope"));
            if (hit.collider != null)
            {
                mineObj.transform.position = hit.point;
                mineObj.isCollision = true;
            }
            else
            {
                mineObj.transform.position = transform.position;
            }
            this.GetComponent<SpriteRenderer>().enabled = false;
            mineObj.MineMove(dir, () => { mineObj.gameObject.SetActive(false); mineCount--; this.GetComponent<SpriteRenderer>().enabled = true; });
            mineCount++;
        }
    }
}
