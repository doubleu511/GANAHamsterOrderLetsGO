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

        seq.Append(aObj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-779, -309), 0));
        seq.Join(bObj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(660, -333), 0));

        seq.Append(aObj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-779, -279), .8f)).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        seq.Join(bObj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(660, -303), .8f)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void Update()
    {
        if(Input.anyKeyDown && isLoadScene)
        {
            Global.LoadScene.LoadScene("SampleScene");
        }
    }
}
