using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleDefaultArm : Module
{
    [SerializeField]
    protected Transform[] arms;
    [SerializeField]
    protected List<Vector3> dirs= new List<Vector3>();
    [SerializeField]
    protected float armDistance = .2f;
    [SerializeField]
    protected float armAngle = 90;
    public override void ModuleEquip()
    {
        for (int i = 0; i < arms.Length; i++)
        {
            dirs.Add(new Vector3());
        }
    }

    public override void ModuleUpdate()
    {
        ArmMoving();
    }

    public virtual void ArmMoving()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
      

        for (int i = 0; i < arms.Length; i++)
        {
            // 중심 위치
            Vector3 anchorPos = arms[i].parent.position/*GameManager.Player.gameObject.transform.position*/;
            // 중심에서 마우스 방향으로의 백터값의 노말
            dirs[i] = (pos - anchorPos).normalized;
            // 팔의 위치 = 중심위치에서 거리만큼 방향으로 이동한 값
            arms[i].position = anchorPos + Vector3.ClampMagnitude(dirs[i], armDistance);
            // 팔의 각도
            var angle = Mathf.Atan2(dirs[i].y, dirs[i].x) * Mathf.Rad2Deg;
            arms[i].rotation = Quaternion.AngleAxis(angle + armAngle, Vector3.forward);
        }

    }
    public override void ModuleUnequip()
    {

    }
}
