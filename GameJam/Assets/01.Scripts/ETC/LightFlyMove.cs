using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightFlyMove : MonoBehaviour
{
    private void Start()
    {

        float pos = transform.position.y;
        transform.DOMoveY(pos - .2f, 0);
        transform.DOMoveY(pos, .8f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
       
    }
}
