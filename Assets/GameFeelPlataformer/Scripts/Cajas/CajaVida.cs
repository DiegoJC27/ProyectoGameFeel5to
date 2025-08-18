using UnityEngine;

public class CajaVida : Caja
{
    public GameObject vidaPrefab;
    public int vida = 1;

    public override void Romper(TipoImpacto tipo)
    {
        base.Romper(tipo);
        // SFX: destruir
        PlayImpactSound(TipoImpacto.Golpear);

        if (vida > 0)
        {
            Instantiate(vidaPrefab, transform.position, Quaternion.identity);
            vida--;
        }

    }
}
