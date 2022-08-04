using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
    private static CameraMove Instance { get; set; }

    private CinemachineVirtualCamera followCam;
    private CinemachineBasicMultiChannelPerlin bPerlin;
    private CinemachineTransposer transposer;
    private Vector3 defaultFollowOffset;

    private Tween offsetTween;

    private bool isShake = false;
    private float currentTime = 0f; // 흔들리는 시간

    public RectTransform cinematic_top;
    public RectTransform cinematic_bottom;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("다수의 카메라 스크립트가 실행중입니다.");
        }
        Instance = this;
        followCam = GetComponent<CinemachineVirtualCamera>();
        bPerlin = followCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        transposer = followCam.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void Start()
    {
        defaultFollowOffset = transposer.m_FollowOffset;
        bPerlin.m_AmplitudeGain = 0;
    }

    public void ShakeCamTL(float intensity)
    {
        ShakeCam(intensity, 0.3f);
    }

    public static void ShakeCam(float intensity, float time)
    {
        // 코루틴 호출
        if (!Instance.isShake)
        {
            Instance.isShake = true;
        }
        else
        {
            Instance.bPerlin.m_AmplitudeGain = intensity;
            Instance.currentTime = time;
        }
    }

    public static void ZoomCam(float zoom, float time)
    {
        DOTween.To(() => Instance.followCam.m_Lens.OrthographicSize, value => Instance.followCam.m_Lens.OrthographicSize = value,
            zoom, time);
    }

    public static void OffsetCam(Vector3 offset, float time)
    {
        Instance.offsetTween.Kill();
        Instance.offsetTween = DOTween.To(() => Instance.transposer.m_FollowOffset, value => Instance.transposer.m_FollowOffset = value,
            offset, time).OnComplete(() =>
            {
                DOTween.To(() => Instance.transposer.m_FollowOffset, value => Instance.transposer.m_FollowOffset = value,
                Instance.defaultFollowOffset, 1);
            });
    }

    public static void CinematicBar(bool appear, float time)
    {
        if (appear)
        {
            Instance.cinematic_top.DOSizeDelta(new Vector2(Instance.cinematic_top.sizeDelta.x, 120), time);
            Instance.cinematic_bottom.DOSizeDelta(new Vector2(Instance.cinematic_bottom.sizeDelta.x, 120), time);
        }
        else
        {
            Instance.cinematic_top.DOSizeDelta(new Vector2(Instance.cinematic_top.sizeDelta.x, 0), time);
            Instance.cinematic_bottom.DOSizeDelta(new Vector2(Instance.cinematic_bottom.sizeDelta.x, 0), time);
        }
    }

    public IEnumerator ShakeUpdate(float intensity, float time)
    {
        bPerlin.m_AmplitudeGain = intensity;
        currentTime = 0;

        while (true)
        {
            yield return null;
            currentTime += Time.deltaTime;
            if (currentTime >= time)
            {
                break;
            }
            bPerlin.m_AmplitudeGain = Mathf.Lerp(intensity, 0f, currentTime / time);
        }
        isShake = false;

        bPerlin.m_AmplitudeGain = 0;
    }
}