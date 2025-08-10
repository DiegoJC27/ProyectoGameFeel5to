using UnityEngine;

public abstract class Caja : MonoBehaviour
{
    public enum TipoImpacto { Golpear, Saltar }

    [Header("Audio")]
    [SerializeField] private AudioClip bounceClip;   // Sonido al rebotar (Saltar)
    [SerializeField] private AudioClip destroyClip;  // Sonido al romper (Golpear)
    [SerializeField, Range(0f, 1f)] private float volume = 1f;
    [SerializeField] private bool spatialSound = false; // false=2D, true=3D
    [SerializeField, Range(0f, 0.1f)] private float pitchJitter = 0.02f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 15f;

    /// Llama esto en los hijos cuando reciban impacto.
    protected void PlayImpactSound(TipoImpacto tipo, float volumeMul = 1f)
    {
        AudioClip clip = (tipo == TipoImpacto.Saltar) ? bounceClip : destroyClip;
        if (clip == null) return;

        // GameObject temporal para no cortar el audio aunque se destruya la caja
        var go = new GameObject("OneShot_Caja");
        var src = go.AddComponent<AudioSource>();
        src.clip = clip;
        src.volume = Mathf.Clamp01(volume * volumeMul);
        src.spatialBlend = spatialSound ? 1f : 0f;

        if (spatialSound)
        {
            src.rolloffMode = AudioRolloffMode.Linear;
            src.minDistance = minDistance;
            src.maxDistance = maxDistance;
        }

        // Variación sutil
        src.pitch = 1f + Random.Range(-pitchJitter, pitchJitter);

        go.transform.position = transform.position;
        src.Play();
        Destroy(go, clip.length / Mathf.Max(0.01f, src.pitch));
    }

    public abstract void Romper(TipoImpacto tipo);
}
