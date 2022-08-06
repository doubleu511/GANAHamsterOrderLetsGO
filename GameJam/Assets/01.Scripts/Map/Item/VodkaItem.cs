using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VodkaItem : CollisionItem
{
    private SpriteRenderer sr;

    protected override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
    }

    public override void OnEnter()
    {
        SubtitlePanel subtitle = FindObjectOfType<SubtitlePanel>();
        subtitle.ShowSubtitle(subtitle.onObtainVodka);

        transform.DOKill();
        transform.DOMoveY(1, 1).SetRelative();
        sr.DOColor(new Color(1, 1, 1, 0), 1).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
