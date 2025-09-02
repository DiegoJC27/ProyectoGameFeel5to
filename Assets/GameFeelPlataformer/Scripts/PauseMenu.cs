using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Audio;
public class PauseMenu : MonoBehaviour
{
    public KeyCode pauseKey;
    public CanvasGroup canvasGroup;

    private bool gamePaused;
    private const float TWEEN_TIME = 0.3f;
    private Tween pauseTween;
    public SoundManager uiSoundManager;

    public GameObject buttons;

    public AudioMixerGroup MasterVolume, MusicVolume, SFXVolume;
    void Start()
    {
        gamePaused = false;
        canvasGroup.alpha = 0;
        buttons.SetActive(false);
        uiSoundManager.PlaySound("GameplayTheme");

    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        { 
            TogglePause(!gamePaused);
            if (gamePaused)
            {
                uiSoundManager?.FadeOutSound("GameplayTheme", 1);
            }
            else
            {
                uiSoundManager?.FadeInSound("GameplayTheme", 1);
            }            
        }
    }
    public void TogglePause(bool paused)
    {
        Time.timeScale = paused ? 0 : 1;

        buttons.SetActive(paused);

        float canvasAlpha = paused ? 1 : 0;

        pauseTween?.Kill();

        pauseTween = canvasGroup.DOFade(canvasAlpha, TWEEN_TIME).SetUpdate(true);
        gamePaused = paused;
    }
    public void SetMasterVolume(float volume)
    {
        MasterVolume.audioMixer.SetFloat("MixerMasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        MusicVolume.audioMixer.SetFloat("MixerMusicVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        SFXVolume.audioMixer.SetFloat("MixerSFXVolume", volume);
    }

    public void PlayClickSound()
    {
        uiSoundManager?.PlaySound("ClickButton");
    }
}
