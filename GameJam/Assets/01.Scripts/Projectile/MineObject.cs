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
    public void MineMove(Vector3 dir, Action act)
    {
        this.act = act;
        this.act += () => {
            isCollision = false;
            isBomb = false;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        };
        StartCoroutine(MoveProcess(dir));
    }
    

    private IEnumerator MoveProcess(Vector3 dir)
    {
        int count = 0;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            count++;
            if (!isCollision)
            {
                transform.position += dir.normalized * Time.deltaTime * moveSpeed;
                Debug.DrawRay(transform.position, dir.normalized ,Color.red,.1f);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, .1f, (1 << LayerMask.NameToLayer("Ground") )+ ( 1<<  LayerMask.NameToLayer("Slope")));
                if (hit.collider != null)
                {
                    print(hit.transform.name);
                    Vector2 normal = hit.normal;
                    float angle = Vector2.SignedAngle(Vector2.up, normal);
                    print(angle);
                    Debug.DrawRay(hit.transform.position, hit.normal, Color.red, 10.0f);
                    Vector3 rotation = transform.eulerAngles + new Vector3(0, 0, angle);
                    transform.rotation = Quaternion.Euler(rotation);
                    isCollision = true;
                    /*if (angle > 150)
                    {
                        transform.position -= new Vector3(0, 0.125f, 0);
                    }
                    else if (angle <= 0)
                    {
                        transform.position += new Vector3(0, 0.125f, 0);

                    }*/
                    StartCoroutine(collProcess());
                 
                }
            }
            if (count > 500 )
            {
                act?.Invoke();
                break;
            }
        }

    }
    private IEnumerator collProcess()
    {
        yield return new WaitForSeconds(.3f);
        isBomb = true;
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
                act?.Invoke();
            }
        }
        /*else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Slope"))
        {
            isCollision = true;
            StartCoroutine(collProcess());
        }*/
    }
}
