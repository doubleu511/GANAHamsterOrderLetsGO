using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleYoYoArm : ModuleDefaultArm
{
    // ���� ������ �ѹ��� �߻簡��
    //
    // ��䰡 �ٽ� ���ƿ��� ��Ȳ
    /*
    1. ȭ�� �ٱ����� ������ ��
    2. ��並 ���� �̵��� ������ ��
    */

    public float _yoyoSpeed = 6f;
    public float _yoyoDistance = 10f;
    private int _yoyoCount = 0;

    private Action yoyoCallback;
    private YoYoObject yoyo;

    private void Start()
    {
        yoyo = GetComponent<YoYoObject>();

        if (yoyo == null)
        {
            Debug.LogWarning("YoYoObject ��ũ��Ʈ�� �����");
        }

        GameManager.Player.OnGroundCollision += () => { _yoyoCount = 0; };

        yoyoCallback = () =>
       {
           if (GameManager.Player.IsGround)
               _yoyoCount = 0;

           GameManager.Player.Rigid.velocity = new Vector2(GameManager.Player.Rigid.velocity.x, 0);
       };
    }

    public override void ArmMoving()
    {
        if (_yoyoCount == 0)
            base.ArmMoving();

        if (Input.GetMouseButtonDown(0) && _yoyoCount == 0)
        {
            Vector3 playerPos = GameManager.Player.transform.position;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3 shotDir = (mousePos - playerPos).normalized;

            yoyo.YoYoShot(shotDir, _yoyoSpeed, _yoyoDistance, yoyoCallback);
            _yoyoCount++;
        }
    }
}
