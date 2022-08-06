using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModuleRocketLauncherArm : ModuleDefaultArm
{
    public GameObject RocketPrefab;
    private int RocketCount = 0;
    [SerializeField]
    private Sprite[] onImg;
    [SerializeField]
    private Sprite[] offImg;
    [SerializeField]
    private GameObject[] lightObj;
    private void Start()
    {

        GameManager.Player.OnGroundCollision += () =>
        {
            RocketCount = 0;
            for (int i = 0; i < arms.Length; i++)
            {
                lightObj[i].SetActive(true);
                arms[i].GetComponent<SpriteRenderer>().sprite = onImg[i];
            }
        };
    }
    public override void ArmMoving()
    {
        base.ArmMoving();
        if(Input.GetMouseButtonDown(0) && RocketCount == 0 && !EventSystem.current.IsPointerOverGameObject())
        {
           
            GameManager.Player.Rigid.velocity = Vector2.zero;
            for (int i = 0; i < arms.Length; i++)
            {
                GameManager.Player.Rigid.AddForce((dirs[i] * -1) * 5, ForceMode2D.Impulse);
                arms[i].GetComponent<SpriteRenderer>().sprite = offImg[i];
                lightObj[i].SetActive(false);
                RocketCount++;
            }

            // 로켓런처 발사 사운드 + 이펙트
        }
    }
}
