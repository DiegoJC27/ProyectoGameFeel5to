using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource loopSource; // Para sonidos en loop (correr)
    [SerializeField] private AudioSource sfxSource;  // Para efectos puntuales

    [Header("Audio Clips")]
    public AudioClip s_Run;
    public AudioClip s_Jump;
    public AudioClip s_Attack;
    public AudioClip s_GroundPound;

    private bool isRunLoopPlaying = false;

    void Start()
    {
        // Configuración básica
        if (loopSource != null)
        {
            loopSource.loop = true;
            loopSource.playOnAwake = false;
            loopSource.clip = null;
        }
        if (sfxSource != null)
        {
            sfxSource.loop = false;
            sfxSource.playOnAwake = false;
        }
    }

    // --- Métodos para reproducir sonidos ---
    public void PlayJump()
    {
        if (sfxSource && s_Jump) sfxSource.PlayOneShot(s_Jump);
    }

    public void PlayAttack()
    {
        if (sfxSource && s_Attack) sfxSource.PlayOneShot(s_Attack);
    }

    public void PlayGroundPound()
    {
        if (sfxSource && s_GroundPound) sfxSource.PlayOneShot(s_GroundPound);
    }

    public void PlayRunLoop()
    {
        if (!isRunLoopPlaying && loopSource && s_Run)
        {
            loopSource.clip = s_Run;
            loopSource.Play();
            isRunLoopPlaying = true;
        }
    }

    public void StopRunLoop()
    {
        if (isRunLoopPlaying && loopSource)
        {
            loopSource.Stop();
            isRunLoopPlaying = false;
        }
    }
}