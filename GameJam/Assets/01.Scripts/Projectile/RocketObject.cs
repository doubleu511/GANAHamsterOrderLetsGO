using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RocketObject : MonoBehaviour
{
    private float rocketSpeed = 2;
    public void RocketMove(Vector3 dir,Action act)
    {
        StartCoroutine(MoveProcess(dir, act));
    }

    private IEnumerator MoveProcess(Vector3 dir,Action act)
    {
        int count = 0;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            count++;
            transform.position += dir * Time.deltaTime * rocketSpeed;
            if(count > 1000)
            {
                break;
            }
        }
        act?.Invoke();
        
    }
}
