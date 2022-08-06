using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

public class SubtitlePanel : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;

    private CanvasGroup canvasGroup;
    private bool isFinished = false;
    private bool isSubtitleLaunched = false;
    private float subtitleableTimer = 30f;
    private Coroutine subtitleCoroutine;

    [Header("ÀÚ¸·µé")]
    public SubtitleSO onFirstStart;
    public SubtitleSO onReenter;
    public SubtitleSO onExit;

    public SubtitleSO onObtainHammer;
    public SubtitleSO onObtainPropeller;
    public SubtitleSO onObtainVodka;

    public SubtitleSO onEnterCatPlace;
    public SubtitleSO onFallDown;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if(!isSubtitleLaunched)
        {
            subtitleableTimer += Time.deltaTime;
        }
    }

    public void ShowSubtitle(SubtitleSO subtitle, UnityAction startCallback = null, UnityAction endCallback = null)
    {
        if (!subtitle.isSkipable)
        {
            if (isSubtitleLaunched) return;
            if (subtitleableTimer < 30) return;
        }
        else
        {
            StopSubtitleCoroutine();
        }

        if(startCallback != null)
        {
            startCallback.Invoke();
        }

        isSubtitleLaunched = true;
        subtitleableTimer = 0f;

        int randomIdx = Random.Range(0, subtitle.subtitles.Length);
        Subtitle currentSub = subtitle.subtitles[randomIdx];
        subtitleCoroutine = StartCoroutine(CoSubtitle(currentSub, endCallback));
    }

    public IEnumerator CoSubtitle(Subtitle sub, UnityAction callback = null)
    {
        for (int i = 0; i < sub.texts.Length; i++)
        {
            isFinished = false;

            subtitleText.text = sub.texts[i];
            int len = TextLength(sub.texts[i]);

            Global.UI.UIFade(canvasGroup, Define.UIFadeType.IN, 1, false);
            float time = Mathf.Clamp(len * 0.1f, 2.5f, len * 0.1f);
            yield return new WaitForSeconds(time);
            Global.UI.UIFade(canvasGroup, Define.UIFadeType.OUT, 1, false, () =>
            {
                isFinished = true;
            });
            yield return new WaitUntil(() => isFinished);
        }

        yield return new WaitForSeconds(0.5f);
        isSubtitleLaunched = false;
        if (callback != null)
        {
            callback.Invoke();
        }
    }

    private void StopSubtitleCoroutine()
    {
        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
        }
        isFinished = false;
        isSubtitleLaunched = false;
        subtitleableTimer = 0f;

        canvasGroup.DOComplete();
        Global.UI.UIFade(canvasGroup, false);
    }

    private int TextLength(string richText)
    {
        int len = 0;
        bool inTag = false;

        foreach (var ch in richText)
        {
            if (ch == '<')
            {
                inTag = true;
                continue;
            }
            else if (ch == '>')
            {
                inTag = false;
            }
            else if (!inTag)
            {
                len++;
            }
        }

        return len;
    }
}
