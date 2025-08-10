using UnityEngine;

public class MaskScript : Collectibles
{
    [SerializeField] private AudioClip maskClip; // sonido al recoger la máscara
    [SerializeField] private float volume = 1f;  // volumen opcional

    public override void OnCollideWithPlayer()
    {
        GameManager.instance.CollectMask();

        if (maskClip != null)
        {
            AudioSource.PlayClipAtPoint(maskClip, transform.position, volume);
        }

        Destroy(gameObject);
    }
}
