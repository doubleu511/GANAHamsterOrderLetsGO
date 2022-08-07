using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionItem : CollisionItem
{
    public int collectionNumber = 0b0000_0000;
    private SpriteRenderer itemSr;
    protected override void Awake()
    {
        base.Awake();
        itemSr = itemTrm.GetComponent<SpriteRenderer>();
    }
    public override void OnEnter()
    {
        int result = GameManager.Game.collectionCount + collectionNumber;
        SecurityPlayerPrefs.SetInt("game.collection", result );

        itemTrm.DOKill();
        itemTrm.DOMoveY(1, 1).SetRelative();
        itemSr.DOColor(new Color(1, 1, 1, 0), 1).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
