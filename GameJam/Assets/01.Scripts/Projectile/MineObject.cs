using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineObject : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3;
    [field: SerializeField]
    public bool isCollision { get; set; } = false;
    [SerializeField]
    private bool isBomb = false;
    
    private Action act;
    [SerializeField]
    private Sprite[] img;
    [SerializeField]
    private GameObject lightObj;
    [SerializeField]
    private ParticleSystem vfx;

    public Rigidbody2D rigid;
    public bool isLeftMine { get; set; } = false;
    public bool isLaunched { get; set; } = false;
    
    public void MineMove(Vector3 dir, Action act)
    {

        StopAllCoroutines();
        this.act = act;
        transform.rotation = Quaternion.Euler(Vector2.zero);
        rigid.velocity = Vector2.zero;
        rigid.AddForce(dir * moveSpeed, ForceMode2D.Impulse);
        StartCoroutine(MoveProcess(dir));
    }
    
    public IEnumerator LifeProcess(float lifeTime,Action act)
    {
        yield return new WaitForSeconds(lifeTime);
        if (!isCollision)
            Bomb(act);
    }
    private IEnumerator MoveProcess(Vector3 dir)
    {
        StartCoroutine(LifeProcess(4, act));
        while (true)
        {
            yield return new WaitForEndOfFrame();
            
            if (!isCollision && isLaunched)
            {
                
                dir = rigid.velocity.normalized;
                rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x, -10, 10), Mathf.Clamp(rigid.velocity.y, -10, 10));
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, .4f, (1 << LayerMask.NameToLayer("Ground")) + (1 << LayerMask.NameToLayer("Slope")));
                if (hit.collider != null)
                {
                    CallOnHit(hit,act);

                }
                
            }
        }

    }
    public void CallOnHit(RaycastHit2D hit,Action act)
    {

        isCollision = true;
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 0;
        //print(hit.transform.name);
        Vector2 normal = hit.normal;
        float angle = Vector2.SignedAngle(Vector2.up, normal);
        transform.position = hit.point;
        //Debug.DrawRay(hit.transform.position, hit.normal, Color.red, 10.0f);
        Vector3 rotation = transform.eulerAngles + new Vector3(0, 0, angle);
        transform.rotation = Quaternion.Euler(rotation);
        transform.position += transform.up * 0.125f;

        StartCoroutine(BombProcess(act));
        StartCoroutine(CollProcess());
    }
    private IEnumerator BombProcess(Action act)
    {

        bool isOn = true;
        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForSeconds(.5f);
            GetComponent<SpriteRenderer>().sprite = img[isOn ? 1:0];
            lightObj.SetActive(isOn);
            isOn = !isOn;
        }
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(.05f);
            GetComponent<SpriteRenderer>().sprite = img[isOn ? 1 : 0];
            print(i);
            lightObj.SetActive(isOn);
            isOn = !isOn;
        }
        Bomb(act);
    }
    public IEnumerator CollProcess()
    {
        yield return new WaitForSeconds(.3f);
        isBomb = true;
    }
    public void Bomb(Action act)
    {
        if (!isLaunched) return;
        StopAllCoroutines();
        print("BOMB");
        // Áö·Ú Æø¹ß È¿°ú
        Effect_MineBomb emb = Global.Pool.GetItem<Effect_MineBomb>();
        emb.transform.position = transform.position;
        emb.GetComponent<ParticleSystem>().Play();
        emb.OnDisable += ()=> emb.gameObject.SetActive(false);

        isCollision = false;
        isBomb = false;
        isLaunched = false;

        GetComponent<SpriteRenderer>().sprite = img[0];
        transform.rotation = Quaternion.Euler(Vector3.zero);
        lightObj.SetActive(false);
        rigid.gravityScale = 1;

        act?.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            if(isBomb)
            {
                Vector3 dir = collision.transform.position - transform.position;
                GameManager.Player.Rigid.velocity = new Vector2(0, 0);
                GameManager.Player.Rigid.AddForce(dir.normalized * 10, ForceMode2D.Impulse);
                Bomb(act);
            }
        }
        /*else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Slope"))
        {
            isCollision = true;
            StartCoroutine(CollProcess());
            StartCoroutine(BombProcess());
        }*/
    }
}
