using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PauseMenu : MonoBehaviour
{
    public KeyCode pauseKey;
    public CanvasGroup canvasGroup;

    private bool gamePaused;
    private const float TWEEN_TIME = 0.3f;
    private Tween pauseTween;

    public GameObject buttons;
    void Start()
    {
        gamePaused = false;
        canvasGroup.alpha = 0;
        buttons.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
            TogglePause(!gamePaused);
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
}
