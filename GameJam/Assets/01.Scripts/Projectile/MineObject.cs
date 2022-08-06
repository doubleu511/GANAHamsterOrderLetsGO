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

    private Rigidbody2D rigid;
    
    public void MineMove(Vector3 dir, Action act)
    {
        rigid = GetComponent<Rigidbody2D>();

        this.act = act;
        this.act += () => {
            isCollision = false;
            isBomb = false;
            
            GetComponent<SpriteRenderer>().sprite = img[0];
            transform.rotation = Quaternion.Euler(Vector3.zero);
            lightObj.SetActive(false);
            rigid.gravityScale = 1;
            gameObject.SetActive(false);
        };
        rigid.AddForce(dir * moveSpeed, ForceMode2D.Impulse);
        StartCoroutine(MoveProcess(dir));
    }
    

    private IEnumerator MoveProcess(Vector3 dir)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            
            if (!isCollision)
            {
                dir = rigid.velocity.normalized;
                Debug.DrawRay(transform.position, dir.normalized, Color.red, .2f);
                rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x, -10, 10), Mathf.Clamp(rigid.velocity.y, -10, 10));
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, .4f, (1 << LayerMask.NameToLayer("Ground")) + (1 << LayerMask.NameToLayer("Slope")));
                if (hit.collider != null)
                {
                    isCollision = true;
                    rigid.velocity = Vector2.zero;
                    rigid.gravityScale = 0;
                    print(hit.transform.name);
                    Vector2 normal = hit.normal;
                    float angle = Vector2.SignedAngle(Vector2.up, normal);
                    print(angle);
                    transform.position = hit.point;
                    Debug.DrawRay(hit.transform.position, hit.normal, Color.red, 10.0f);
                    Vector3 rotation = transform.eulerAngles + new Vector3(0, 0, angle);
                    transform.rotation = Quaternion.Euler(rotation);
                    transform.position += transform.up * 0.125f;

                    StartCoroutine(BombProcess());
                    StartCoroutine(CollProcess());

                }
                /* transform.position += dir.normalized * Time.deltaTime * moveSpeed;
                 Debug.DrawRay(transform.position, dir.normalized ,Color.red,.2f);
                 RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, .4f, (1 << LayerMask.NameToLayer("Ground") )+ ( 1<<  LayerMask.NameToLayer("Slope")));
                 if (hit.collider != null)
                 {
                     isCollision = true;
                     print(hit.transform.name);
                     Vector2 normal = hit.normal;
                     float angle = Vector2.SignedAngle(Vector2.up, normal);
                     print(angle);
                     transform.position = hit.point;
                     Debug.DrawRay(hit.transform.position, hit.normal, Color.red, 10.0f);
                     Vector3 rotation = transform.eulerAngles + new Vector3(0, 0, angle);
                     transform.rotation = Quaternion.Euler(rotation);
                     transform.position += transform.up * 0.125f;

                     StartCoroutine(BombProcess());
                     StartCoroutine(CollProcess());

                 }*/
            }
        }

    }
    private IEnumerator BombProcess()
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
            lightObj.SetActive(isOn);
            isOn = !isOn;
        }
        Bomb();
    }
    public IEnumerator CollProcess()
    {
        yield return new WaitForSeconds(.3f);
        isBomb = true;
    }
    public void Bomb()
    {
        print("BOMB");
        // Áö·Ú Æø¹ß È¿°ú
        Effect_MineBomb emb = Global.Pool.GetItem<Effect_MineBomb>();
        emb.transform.position = transform.position;
        emb.GetComponent<ParticleSystem>().Play();
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
                Bomb();
            }
        }
        /*else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Slope"))
        {
            isCollision = true;
            StartCoroutine(collProcess());
        }*/
    }
}
