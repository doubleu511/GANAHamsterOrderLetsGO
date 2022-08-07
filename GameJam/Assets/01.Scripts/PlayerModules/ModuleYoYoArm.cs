using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModuleYoYoArm : ModuleDefaultArm
{
    // 요요는 언제나 한번만 발사가능
    //
    // 요요가 다시 돌아오는 상황
    /*
    1. 화면 바깥으로 나갔을 때
    2. 요요를 통해 이동이 끝났을 때
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
            Debug.LogWarning("YoYoObject 스크립트가 없어요");
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

           GameManager.Player.Rigid.velocity = Vector2.zero;
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
