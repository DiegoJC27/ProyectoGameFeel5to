using UnityEngine;

public class MangoScript : Collectibles
{
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f; // 2D, o ponlo en 1f si quieres que sea 3D
        }
    }

    public override void OnCollideWithPlayer()
    {
        GameManager.instance.CollectMango();

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            Destroy(gameObject, audioSource.clip.length); // Espera a que termine el sonido
        }
        else
        {
            Destroy(gameObject); // Si no hay audio, destruye normal
        }
    }
}

