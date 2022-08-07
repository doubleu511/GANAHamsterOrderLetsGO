using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OptionPanel : MonoBehaviour
{
    private bool isOpen = false;

    private CanvasGroup canvasGroup;
    public CanvasGroup panelBlackScreen;
    public CanvasGroup panelCanvasgroup;

    public Button optionButton;
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
            isOpen = false;
            panelCanvasgroup.DOComplete();
            panelCanvasgroup.GetComponent<RectTransform>().DOComplete();

            Global.Sound.Play("SFX/sfx_PopupClose", Define.Sound.Effect);
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
            Global.Sound.Play("SFX/sfx_ButtonClick", Define.Sound.Effect);
            windowScreenButton.interactable = true;
            fullscreenButton.interactable = false;
            SetFullScreen(true);
        });

        windowScreenButton.onClick.AddListener(() =>
        {
            Global.Sound.Play("SFX/sfx_ButtonClick", Define.Sound.Effect);
            fullscreenButton.interactable = true;
            windowScreenButton.interactable = false;
            SetFullScreen(false);
        });

        bool fullScreen = SecurityPlayerPrefs.GetBool("option.fullScreen", false);
        fullscreenButton.interactable = !fullScreen;
        windowScreenButton.interactable = fullScreen;

        if (GameManager.Game)
        {
            gameEndButton.onClick.AddListener(() =>
            {
                Global.Sound.Play("SFX/sfx_ButtonClick", Define.Sound.Effect);
                panelCanvasgroup.DOComplete();
                panelCanvasgroup.GetComponent<RectTransform>().DOComplete();

                Global.UI.UIFade(canvasGroup, false);
                Global.UI.UIFade(panelCanvasgroup, Define.UIFadeType.FLOATOUT, 0.5f, true);
                Global.UI.UIFade(panelBlackScreen, Define.UIFadeType.IN, 1.5f, true);

                GameManager.Player.CanMove = false;

                SubtitlePanel subtitle = FindObjectOfType<SubtitlePanel>();
                subtitle.ShowSubtitle(subtitle.onExit, null, () =>
                {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            });
            });
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                isOpen = false;
                panelCanvasgroup.DOComplete();
                panelCanvasgroup.GetComponent<RectTransform>().DOComplete();

                Global.Sound.Play("SFX/sfx_PopupClose", Define.Sound.Effect);
                Global.UI.UIFade(canvasGroup, false);
                Global.UI.UIFade(panelCanvasgroup, Define.UIFadeType.FLOATOUT, 0.5f, true);
            }
            else
            {
                if (!WorkPlace.IsWorkPlaceOpen)
                {
                    OptionStart();
                }
            }
        }
    }
    
    public void OptionStart()
    {
        Global.Sound.Play("SFX/sfx_ButtonClick", Define.Sound.Effect);
        panelCanvasgroup.DOComplete();
        panelCanvasgroup.GetComponent<RectTransform>().DOComplete();

        Global.UI.UIFade(canvasGroup, true);
        Global.UI.UIFade(panelCanvasgroup, true);
        isOpen = true;
    }

    public static void SetFullScreen(bool value)
    {
        SecurityPlayerPrefs.SetBool("option.fullScreen", value);
        Screen.fullScreen = value;

        if(!value)
        {
            Screen.SetResolution(1600, 900, Screen.fullScreen);
        }
        else
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }
    }
}
