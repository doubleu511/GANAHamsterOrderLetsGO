using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleYoYoArm : ModuleDefaultArm
{
    // 요요는 언제나 한번만 발사가능
    //
    // 요요가 다시 돌아오는 상황
    /*
    1. 화면 바깥으로 나갔을 때
    2. 요요를 통해 이동이 끝났을 때
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
            Debug.LogWarning("YoYoObject 스크립트가 없어요");
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
