using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleDefaultArm : Module
{
    [SerializeField]
    protected Transform[] arms;
    [SerializeField]
    protected List<Vector3> dirs= new List<Vector3>();
    protected float armDistance = .2f;
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
            Vector3 anchorPos = arms[i].parent.position/*GameManager.Player.gameObject.transform.position*/;
            dirs[i] = (pos - anchorPos).normalized;
            arms[i].position = anchorPos + Vector3.ClampMagnitude(dirs[i], armDistance);

            var angle = Mathf.Atan2(dirs[i].y, dirs[i].x) * Mathf.Rad2Deg;
            arms[i].rotation = Quaternion.AngleAxis(angle + armAngle, Vector3.forward);
        }

    }
    public override void ModuleUnequip()
    {

    }
}
