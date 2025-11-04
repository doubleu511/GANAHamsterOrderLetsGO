using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModuleYoYoArm : ModuleDefaultArm
{
    // ���� ������ �ѹ��� �߻簡��
    //
    // ��䰡 �ٽ� ���ƿ��� ��Ȳ
    /*
    1. ȭ�� �ٱ����� ������ ��
    2. ��並 ���� �̵��� ������ ��
    */

    public YoYoObject yoyo;
    public float _yoyoSpeed = 6f;
    public float _yoyoDistance = 10f;
    private int _yoyoCount = 0;
    private bool _yoyoReturn = false;

    private Action yoyoCallback;

    private void Start()
    {
        if (yoyo == null)
        {
            Debug.LogWarning("YoYoObject ��ũ��Ʈ�� �����");
        }

        //GameManager.Player.OnGroundCollision += () => 
        //{ 
        //    _yoyoCount = 0;
        //};

        yoyoCallback = () =>
       {
           if (GameManager.Player.IsGround)
           {
               _yoyoCount = 0;
           }
           else
           {
               _yoyoReturn = true;
           }

           GameManager.Player.Rigid.linearVelocity = Vector2.zero;
       };
    }

    public override void ArmMoving()
    {
        if (_yoyoCount == 0)
            base.ArmMoving();

        if(_yoyoReturn && GameManager.Player.IsGround && _yoyoCount == 1)
        {
            _yoyoCount = 0;
            _yoyoReturn = false;
        }

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0) && _yoyoCount == 0)
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
