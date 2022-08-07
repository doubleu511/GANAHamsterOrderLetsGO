using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private Transform charaTrm;

    [SerializeField]
    private Button gameStartButton;

    [SerializeField]
    private Button creditButton;

    [SerializeField]
    private CanvasGroup creditPanel;

    [SerializeField]
    private Button creditCancelButton;

    [SerializeField]
    private Button gameExitButton;

    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        RectTransform charaRect = charaTrm.GetComponent<RectTransform>();

        seq.Append(charaRect.DOAnchorPos(new Vector2(charaRect.anchoredPosition.x, charaRect.anchoredPosition.y - 20), 0));
        seq.Append(charaRect.DOAnchorPos(new Vector2(charaRect.anchoredPosition.x, charaRect.anchoredPosition.y), .8f)).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);

        Global.Sound.Play("BGM/bgm_music0", Define.Sound.Bgm);

        gameStartButton.onClick.AddListener(() =>
        {
            Global.LoadScene.LoadScene("SampleScene");
            Global.Sound.Stop("BGM/bgm_music0", Define.Sound.Bgm);
        });

        creditButton.onClick.AddListener(() =>
        {
            Global.UI.UIFade(creditPanel, true);
            Global.Sound.Play("SFX/sfx_ButtonClick", Define.Sound.Effect);
        });

        creditCancelButton.onClick.AddListener(() =>
        {
            Global.UI.UIFade(creditPanel, false);
            Global.Sound.Play("SFX/sfx_PopupClose", Define.Sound.Effect);
        });

        gameExitButton.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }
}
