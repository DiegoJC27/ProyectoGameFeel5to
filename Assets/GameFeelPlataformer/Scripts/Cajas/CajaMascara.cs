using UnityEngine;

public class CajaMascara : Caja
{
    public GameObject mascaraPrefab;
    public int vida = 1;

    public override void Romper(TipoImpacto tipo)
    {
        // SFX: destruir
        PlayImpactSound(TipoImpacto.Golpear);

        Instantiate(mascaraPrefab, transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
    }
}
