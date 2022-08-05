using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오로지 스프라이트 바꿔끼기만을 위한 클래스
/// </summary>
public class PlayerFoot : MonoBehaviour
{
    [Header("LeftFoot")]
    public SpriteRenderer leftFootSpr;
    public GameObject leftFootTrail;

    [Header("RightFoot")]
    public SpriteRenderer rightFootSpr;
    public GameObject rightFootTrail;

    [Header("DefaultFeetSpr")]
    public Sprite leftDefaultFootSpr;
    public Sprite rightDefaultFootSpr;

    private void Awake()
    {
        ResetFeet();
    }

    public void ResetFeet()
    {
        leftFootSpr.sprite = leftDefaultFootSpr;
        rightFootSpr.sprite = rightDefaultFootSpr;

        leftFootTrail.SetActive(false);
        rightFootTrail.SetActive(false);
    }

    public void SetFeet(Sprite leftFeet, Sprite rightFeet)
    {
        leftFootSpr.sprite = leftFeet;
        rightFootSpr.sprite = rightFeet;

        leftFootTrail.SetActive(true);
        rightFootTrail.SetActive(true);
    }
}
