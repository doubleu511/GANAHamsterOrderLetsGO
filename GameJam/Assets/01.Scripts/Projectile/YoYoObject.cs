using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoYoObject : MonoBehaviour
{
    enum YoYoEnum
    {
        isNotCollision,
        IsCollision,
    }

    public LayerMask whatIsGround;
    public GameObject rope;

    private bool isYoYoMovingEnd = false;
    private bool isPlayerMovingEnd = false;

    private YoYoEnum yoyoEnum = YoYoEnum.isNotCollision;

    private Camera mainCam;
    private Action moveCallback;
    private Transform myParent;

    private Vector3 beforePos;
    private Vector3 afterPos;
    private Vector3 playerPos;

    private float yoyoSpeed = 0;
    private float yoyoDistance = 0;
    private float dist = 0;
    private float curTime = 0;

    private void Start()
    {
        myParent = transform.parent;
        mainCam = Camera.main;
        rope = Instantiate(rope);
        rope.transform.parent = this.transform;
    }

    private void Update()
    {
        if(isYoYoMovingEnd || isPlayerMovingEnd)
        {
            YoYoMove();
        }
    }

    public void YoYoShot(Vector3 dir, float yoyoSpeed, float yoyoDistance, Action callback = null)
    {
        Global.Sound.Play("SFX/sfx_YoYoShot");

        RaycastHit2D yoyoHit = Physics2D.Raycast(GameManager.Player.transform.position, dir, yoyoDistance, whatIsGround);

        if(yoyoHit.collider != null)
        {
            yoyoEnum = YoYoEnum.IsCollision;
            afterPos = yoyoHit.point;
            dist = Vector3.Distance(transform.position, yoyoHit.point);
        }
        else
        {
            yoyoEnum = YoYoEnum.isNotCollision;
            afterPos = (GameManager.Player.transform.position + dir * yoyoDistance);
            dist = 10;
        }

        transform.parent = null;
        transform.localScale = Vector3.one; // 아주 간편 코드 굿
        isYoYoMovingEnd = true;
        isPlayerMovingEnd = true;
        this.yoyoSpeed = yoyoSpeed;
        this.yoyoDistance = yoyoDistance;
        beforePos = transform.position;
        moveCallback = callback;
        rope.SetActive(true);
    }

    private void YoYoMove()
    {
        curTime += Time.deltaTime / dist * (isYoYoMovingEnd ? 1.4f : 1f);
        float t = Mathf.Clamp(curTime * yoyoSpeed, 0, 1);

        if (isYoYoMovingEnd)
        {
            transform.position = Vector3.Lerp(beforePos, afterPos, t);

            if (Vector3.Distance(GameManager.Player.transform.position, transform.position) > yoyoDistance
             || IsCameraInObject())
            {
                playerPos = GameManager.Player.transform.position;
                curTime = 0;
                isYoYoMovingEnd = false;
                yoyoEnum = YoYoEnum.isNotCollision;
            }

            if (t >= 1)
            {
                playerPos = GameManager.Player.transform.position;
                curTime = 0; 
                isYoYoMovingEnd = false;

                RaycastHit2D hit = Physics2D.Raycast(playerPos, (afterPos - playerPos).normalized, yoyoDistance, whatIsGround);

                if(hit.collider != null)
                {
                    afterPos = (Vector3)hit.point - (((Vector3)hit.point - playerPos).normalized * 0.5f);
                    dist = Vector3.Distance(playerPos, afterPos);
                }

                if(yoyoEnum == YoYoEnum.IsCollision)
                {
                    Global.Sound.Play("SFX/sfx_YoYoCollision");
                    Global.Sound.PlayNotOne("SFX/sfx_YoYoGrap", NotOneShot.YoYoGrap);
                }
            }
        }
        else if(isPlayerMovingEnd)
        {
            
            if (yoyoEnum == YoYoEnum.IsCollision)
            {
                if (GameManager.Player.JumpCount == 0)
                    GameManager.Player.ResetJumpCharge(); // 버그가 죽기를 기원하며 함수 루프 돌리기


                GameManager.Player.Rigid.velocity = Vector2.one; // 관련 이벤트가 생겨서 이 코드 넣음
                GameManager.Player.transform.position = Vector3.Lerp(playerPos, afterPos, t);

                if (t >= 1)
                {
                    Global.Sound.StopNotOne("SFX/sfx_YoYoGrap", NotOneShot.YoYoGrap);
                    End();
                }
            }
            else if(yoyoEnum == YoYoEnum.isNotCollision)
            {
                transform.position = Vector3.Lerp(afterPos, GameManager.Player.transform.position, t * 1.5f);

                if (t * 1.5f >= 1)
                {
                    End();
                }
            }
        }

        rope.transform.position = Vector3.Lerp(GameManager.Player.transform.position, transform.position, 0.5f);
        // 언제나 하드코딩을 실생활에서 사용 할수 있도록 하자.
        rope.transform.rotation = Quaternion.LookRotation((GameManager.Player.transform.position - transform.position).normalized) * Quaternion.Euler(90, 0, 0);
        rope.transform.localRotation *= Quaternion.Euler(0, -90, 0);
        rope.transform.localScale = new Vector3(0.05f, Vector3.Distance(GameManager.Player.transform.position, transform.position), 1f);
    }

    private void End()
    {
        moveCallback?.Invoke();
        isPlayerMovingEnd = false;
        transform.parent = myParent;
        curTime = 0;
        transform.localPosition = new Vector2(0.35f, -0.175f);
        transform.localScale = Vector3.one; // 아주 간편 코드 굿
        rope.SetActive(false);
    }

    private bool IsCameraInObject()
    {
        Vector3 viewPos = mainCam.WorldToViewportPoint(transform.position);

        if (!(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0))
        {
            return true;
        }

        return false;
    }
}
