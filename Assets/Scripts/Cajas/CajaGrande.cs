using UnityEngine;

public class CajaGrande : Caja
{
    public GameObject mangoPrefab;
    public int mangosSaltando = 2;
    public int mangosGolpe = 4;
    public int vida = 4;
    public override void Romper(TipoImpacto tipo)
    {
        if (tipo == TipoImpacto.Saltar)
        {
            vida--;

            for (int i = 0; i < mangosSaltando; i++)
            {
                GameManager.instance.CollectMango();
            }

            if (vida <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (tipo == TipoImpacto.Golpear)
        {
            for (int i = 0; i < mangosGolpe; i++)
            {
                Instantiate(mangoPrefab, transform.position + Vector3.up, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

}