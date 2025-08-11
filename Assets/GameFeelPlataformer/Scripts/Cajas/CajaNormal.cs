using UnityEngine;

public class CajaNormal : Caja
{
    public GameObject mangoPrefab;
    public int mangosSaltando = 4;
    public int mangosGolpe = 2;
    public int vida = 1;
    public override void Romper(TipoImpacto tipo)
    {
        if (tipo == TipoImpacto.Saltar)
        {
            

            for (int i = 0; i < mangosSaltando; i++)
            {
                GameManager.instance.CollectMango();
            }
            vida--;
            if (vida <= 0)
                Destroy(gameObject);
        }
        else if (tipo == TipoImpacto.Golpear)
        {
            for (int i = 0; i < mangosGolpe; i++)
            {
                Instantiate(mangoPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
    
}