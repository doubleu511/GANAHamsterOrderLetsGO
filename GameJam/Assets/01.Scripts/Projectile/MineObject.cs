using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineObject : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;
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
            if(!isCollision)
            {
                transform.position += dir.normalized * Time.deltaTime * moveSpeed;
            }
            if (count > 1000 )
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
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Slope"))
        {
            isCollision = true;
            StartCoroutine(collProcess());
        }
    }
}
