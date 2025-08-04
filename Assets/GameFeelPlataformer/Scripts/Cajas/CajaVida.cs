using UnityEngine;

public class CajaVida : Caja
{
    public GameObject vidaPrefab;
    public int vida = 1;

    public override void Romper(TipoImpacto tipo)
    {
        if (vida > 0)
        {
            Instantiate(vidaPrefab, transform.position + Vector3.up, Quaternion.identity);
            vida--;
        }
        Destroy(gameObject);
    }
}