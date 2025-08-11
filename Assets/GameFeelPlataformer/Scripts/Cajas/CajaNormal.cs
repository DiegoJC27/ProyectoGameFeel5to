using UnityEngine;

public class CajaNormal : Caja
{
    [Header("Drops")]
    public GameObject mangoPrefab;
    public int mangosSaltando = 4;
    public int mangosGolpe = 2;

    [Header("Vida")]
    public int vida = 1;

    public override void Romper(TipoImpacto tipo)
    {
        if (tipo == TipoImpacto.Saltar)
        {
            PlayImpactSound(TipoImpacto.Saltar); // bounce

            for (int i = 0; i < mangosSaltando; i++)
                GameManager.instance.CollectMango();

            vida--;
            if (vida <= 0)
            {
                PlayImpactSound(TipoImpacto.Golpear); // destroy al morir por salto
                Destroy(gameObject);
            }
        }
        else // Golpear
        {
            PlayImpactSound(TipoImpacto.Golpear); // destroy

            for (int i = 0; i < mangosGolpe; i++)
                Instantiate(mangoPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
