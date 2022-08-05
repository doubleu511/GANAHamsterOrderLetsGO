using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private Transform aObj;
    [SerializeField]
    private Transform bObj;
    [SerializeField]
    private Transform cObj;

    private bool isLoadScene = true;

    private void Start()
    {
        cObj.GetComponent<CanvasGroup>().DOFade(0, .8f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        Sequence seq = DOTween.Sequence();
        RectTransform aRect = aObj.GetComponent<RectTransform>();
        RectTransform bRect = bObj.GetComponent<RectTransform>();

        seq.Append(aRect.DOAnchorPos(new Vector2(aRect.anchoredPosition.x, aRect.anchoredPosition.y - 20), 0));
        seq.Join(bRect.DOAnchorPos(new Vector2(bRect.anchoredPosition.x, bRect.anchoredPosition.y - 20), 0));

        seq.Append(aRect.DOAnchorPos(new Vector2(aRect.anchoredPosition.x, aRect.anchoredPosition.y), .8f)).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        seq.Join(bRect.DOAnchorPos(new Vector2(bRect.anchoredPosition.x, bRect.anchoredPosition.y), .8f)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void Update()
    {
        if(Input.anyKeyDown && isLoadScene)
        {
            Global.LoadScene.LoadScene("SampleScene");
        }
    }
}
