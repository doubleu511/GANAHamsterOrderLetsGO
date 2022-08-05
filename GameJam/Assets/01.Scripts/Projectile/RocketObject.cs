using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RocketObject : MonoBehaviour
{
    [SerializeField]
    private float rocketSpeed = 10;
    private bool isCollision = false;
    public void RocketMove(Vector3 dir,Action act)
    {
        act += () => { isCollision = false; };
        StartCoroutine(MoveProcess(dir.normalized, act));
    }

    private IEnumerator MoveProcess(Vector3 dir,Action act)
    {
        int count = 0;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            count++;
            transform.position += dir * Time.deltaTime * rocketSpeed;
            if(count > 1000 || isCollision)
            {
                act?.Invoke();
                break;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Slope"))
        {
            isCollision = true;
        }
    }
}
