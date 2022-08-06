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
            // �߽� ��ġ
            Vector3 anchorPos = arms[i].parent.position/*GameManager.Player.gameObject.transform.position*/;
            // �߽ɿ��� ���콺 ���������� ���Ͱ��� �븻
            dirs[i] = (pos - anchorPos).normalized;
            // ���� ��ġ = �߽���ġ���� �Ÿ���ŭ �������� �̵��� ��
            arms[i].position = anchorPos + Vector3.ClampMagnitude(dirs[i], armDistance);
            // ���� ����
            var angle = Mathf.Atan2(dirs[i].y, dirs[i].x) * Mathf.Rad2Deg;
            arms[i].rotation = Quaternion.AngleAxis(angle + armAngle, Vector3.forward);
        }

    }
    public override void ModuleUnequip()
    {

    }
}
