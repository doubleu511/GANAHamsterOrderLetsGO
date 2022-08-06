using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (Input.GetMouseButtonDown(0) && mineCount < 2 && !EventSystem.current.IsPointerOverGameObject())
        {
            MineObject mineObj = Global.Pool.GetItem<MineObject>();

            Vector3 anglePos = arms[mineCount].position;


            RaycastHit2D hit = Physics2D.Raycast(anglePos, dirs[mineCount].normalized, Vector3.Distance(anglePos, transform.position), LayerMask.GetMask("Ground") + LayerMask.GetMask("Slope"));
            if (hit.collider != null)
            {

                Vector2 normal = hit.normal;
                float angle = Vector2.SignedAngle(Vector2.up, normal);
                Vector3 rotation = mineObj.transform.eulerAngles + new Vector3(0, 0, angle);
                mineObj.transform.rotation = Quaternion.Euler(rotation);
                mineObj.transform.localPosition = hit.point + new Vector2(0,0.125f);
                mineObj.isCollision = true;
                StartCoroutine(mineObj.CollProcess());

            }
            else
            {
                mineObj.transform.position = arms[mineCount].position;
            }
            arms[mineCount].GetComponent<SpriteRenderer>().enabled = false;
            mineObj.MineMove(dirs[mineCount], () => { mineObj.gameObject.SetActive(false); mineCount--; arms[mineCount].GetComponent<SpriteRenderer>().enabled = true; });
            mineCount++;
            // 지뢰 런처 발사 효과
        }
    }
}
