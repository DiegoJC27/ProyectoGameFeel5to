using UnityEngine;

public class CajaGrande : Caja
{
    public GameObject mangoPrefab;
    public int mangosSaltando = 2;
    public int mangosGolpe = 4;
    public int vida = 4;

    public override void Romper(TipoImpacto tipo)
    {
        base.Romper(tipo);
        if (tipo == TipoImpacto.Saltar)
        {
            // SFX: rebote
            PlayImpactSound(TipoImpacto.Saltar);

            for (int i = 0; i < mangosSaltando; i++)
            {
                GameManager.instance.CollectMango();
            }

            vida--;
            if (vida <= 0)
            {
                // SFX: destruir al morir por salto
                PlayImpactSound(TipoImpacto.Golpear);
                Destroy(gameObject);
            }
        }
        else if (tipo == TipoImpacto.Golpear)
        {
            // SFX: destruir
            PlayImpactSound(TipoImpacto.Golpear);

            for (int i = 0; i < mangosGolpe; i++)
            {
                if (vida > 0)
                {
                    Instantiate(mangoPrefab, transform.position + Vector3.up, Quaternion.identity);
                    vida--;
                }
            }

        }
    }
}
