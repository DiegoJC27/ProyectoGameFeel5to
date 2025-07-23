using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Fader : MonoBehaviour
{
    public Image fadeImage;
    public Color imageColor;
    public UnityEvent onCompleteTweenEvent;
    public float fadeTime;
    private const float DELAY_TIME = 1;
    private void Start()
    {
        fadeImage.raycastTarget = false;
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
        fadeImage.DOFade(0, fadeTime).SetDelay(DELAY_TIME);
    }
    public void FadeToBlack()
    {
        fadeImage.DOFade(1, fadeTime).OnComplete(onCompleteTweenEvent.Invoke);
    }

}
