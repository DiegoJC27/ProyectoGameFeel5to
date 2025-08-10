using UnityEngine;

public class LifeScript : Collectibles
{
    [SerializeField] private AudioClip extraLifeClip; // asigna tu clip aqu�
    [SerializeField] private float volume = 1f;       // volumen opcional

    public override void OnCollideWithPlayer()
    {
        GameManager.instance.CollectLife();

        if (extraLifeClip != null)
        {
            // Reproduce el sonido en la posici�n actual
            AudioSource.PlayClipAtPoint(extraLifeClip, transform.position, volume);
        }

        // Destruye la vida inmediatamente
        Destroy(gameObject);
    }
}
