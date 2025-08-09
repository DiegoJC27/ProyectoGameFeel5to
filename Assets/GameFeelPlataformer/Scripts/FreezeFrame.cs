using System.Collections;
using UnityEngine;

public class FrezeeFrame : MonoBehaviour
{
    private Coroutine freezeFrameRoutine;
    private float timeValue;
    public void FreezeTimeValue(float time)
    {
        timeValue = time;
    }
    public void FreezeFrame(float duration)
    {
        if (freezeFrameRoutine != null)
        {
            StopCoroutine(freezeFrameRoutine);
        }

        freezeFrameRoutine = StartCoroutine(FreezeFrameRoutine(duration));
    }

    IEnumerator FreezeFrameRoutine(float duration)
    {
        Time.timeScale = timeValue;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }
}