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
    [SerializeField]
    private GameObject vfx;
    private void Start()
    {
        Global.Pool.CreatePool<Effect_RocketLaunch>(vfx,GameManager.Game.transform);

        GameManager.Player.OnFallGround += () =>
        {
            RefillRocket();
        };

        GameManager.Player.OnCollisionWall += () =>
        {
            EmptyRocket();
        };
    }
    public override void ArmMoving()
    {
        base.ArmMoving();
        if(Input.GetMouseButtonDown(0) && RocketCount == 0 && !EventSystem.current.IsPointerOverGameObject())
        {
            if(GameManager.Player.IsGround) // ������ ���� �־��ٸ�
            {
                GameManager.Player.JumpCount = 1;
            }

            GameManager.Player.Rigid.velocity = Vector2.zero;
            for (int i = 0; i < arms.Length; i++)
            {
                GameManager.Player.Rigid.AddForce((dirs[i] * -1) * 5, ForceMode2D.Impulse);
                arms[i].GetComponent<SpriteRenderer>().sprite = offImg[i];
                lightObj[i].SetActive(false);
                RocketCount++;
            }
            StartCoroutine(RefillRocketProcess());

            // ���Ϸ�ó �߻� ���� + ����Ʈ
            // ����Ʈ Ǯ ���� ������
            Effect_RocketLaunch erl = Global.Pool.GetItem<Effect_RocketLaunch>();
            // ���콺��ġ�� ���ؼ� ���Ⱚ ����
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = (transform.position - mousePos).normalized;
            // �� �� ������ ��ġ
            Vector2 pos0 = arms[0].position;
            Vector2 pos1 = arms[1].position;
            Vector2 midPos = new Vector2(((pos0.x + pos1.x) / 2),(pos0.y+ pos1.y)/2);
            // ������ ���� ��ġ�� �̵�
            erl.transform.position = midPos + (dir * Vector2.down * 2);
        }
    }
    private IEnumerator RefillRocketProcess()
    {
        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(.01f);
            count++;
            if (!GameManager.Player.IsGround)
            {
                yield break;
            }
            if (count > 400)
            {
                break;
            }

        }
        RefillRocket();

    }
    private void RefillRocket()
    {
        RocketCount = 0;
        for (int i = 0; i < arms.Length; i++)
        {
            lightObj[i].SetActive(true);
            arms[i].GetComponent<SpriteRenderer>().sprite = onImg[i];
        }
    }

    private void EmptyRocket()
    {
        RocketCount = 1;
        for (int i = 0; i < arms.Length; i++)
        {
            lightObj[i].SetActive(false);
            arms[i].GetComponent<SpriteRenderer>().sprite = offImg[i];
        }
    }
}
