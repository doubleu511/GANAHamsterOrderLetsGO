using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class LoadingCanvas : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tmpLoadingText;
    [SerializeField]
    private Transform aObj;
    [SerializeField]
    private Text dummyTxt;
    [SerializeField]
    private Image progressBar;
    private void Update()
    {
        if (Global.LoadScene.operation != null)
            progressBar.rectTransform.localScale = new Vector3(Global.LoadScene.operation.progress, 1, 1);
    }
    private void OnEnable()
    {
        Sequence seq = DOTween.Sequence();
        RectTransform aRect = aObj.GetComponent<RectTransform>();
        seq.Append(aRect.DOAnchorPos(new Vector2(aRect.anchoredPosition.x, aRect.anchoredPosition.y), 0));
        seq.Join(dummyTxt.DOText("·ÎµùÁß...", .8f)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).OnUpdate(()=> { tmpLoadingText.text = dummyTxt.text; });

        seq.Join(aRect.DOAnchorPos(new Vector2(aRect.anchoredPosition.x, aRect.anchoredPosition.y + 10), 1f)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}
