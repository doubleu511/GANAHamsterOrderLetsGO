using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ��������Ʈ �ٲ㳢�⸸�� ���� Ŭ����
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
