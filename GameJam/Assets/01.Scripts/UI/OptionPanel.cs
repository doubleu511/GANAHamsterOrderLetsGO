using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public CanvasGroup panelCanvasgroup;

    public Button cancelButton;

    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    public Button fullscreenButton;
    public Button windowScreenButton;

    public Button gameEndButton;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        cancelButton.onClick.AddListener(() =>
        {
            Global.UI.UIFade(canvasGroup, false);
            Global.UI.UIFade(panelCanvasgroup, Define.UIFadeType.FLOATOUT, 0.5f, true);
        });

        masterVolumeSlider.onValueChanged.AddListener((value) =>
        {
            Global.Sound.SetMasterVolume(value);
        });

        bgmVolumeSlider.onValueChanged.AddListener((value) =>
        {
            Global.Sound.SetVolume(Define.Sound.Bgm, value);
        });

        sfxVolumeSlider.onValueChanged.AddListener((value) =>
        {
            Global.Sound.SetVolume(Define.Sound.Effect, value);
        });

        float masterVolume = Global.Sound.GetMasterVolume();
        float[] volumes = Global.Sound.GetVolumes();

        masterVolumeSlider.value = masterVolume;
        bgmVolumeSlider.value = volumes[(int)Define.Sound.Bgm];
        sfxVolumeSlider.value = volumes[(int)Define.Sound.Effect];

        fullscreenButton.onClick.AddListener(() =>
        {
            windowScreenButton.interactable = true;
            fullscreenButton.interactable = false;
            SetFullScreen(true);
        });

        windowScreenButton.onClick.AddListener(() =>
        {
            fullscreenButton.interactable = true;
            windowScreenButton.interactable = false;
            SetFullScreen(false);
        });

        bool fullScreen = SecurityPlayerPrefs.GetBool("option.fullScreen", true);
        fullscreenButton.interactable = !fullScreen;
        windowScreenButton.interactable = fullScreen;

        gameEndButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    public static void SetFullScreen(bool value)
    {
        SecurityPlayerPrefs.SetBool("option.fullScreen", value);
        Screen.fullScreen = value;

        if(!value)
        {
            Screen.SetResolution(1600, 900, Screen.fullScreen);
        }
    }
}
