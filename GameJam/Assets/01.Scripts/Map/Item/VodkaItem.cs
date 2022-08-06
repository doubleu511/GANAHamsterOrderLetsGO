using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VodkaItem : CollisionItem
{
    private SpriteRenderer itemSr;

    protected override void Awake()
    {
        base.Awake();
        itemSr = itemTrm.GetComponent<SpriteRenderer>();
    }

    public override void OnEnter()
    {
        SubtitlePanel subtitle = FindObjectOfType<SubtitlePanel>();
        subtitle.ShowSubtitle(subtitle.onObtainVodka);

        itemTrm.DOKill();
        itemTrm.DOMoveY(1, 1).SetRelative();
        itemSr.DOColor(new Color(1, 1, 1, 0), 1).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
