using UnityEngine;

public class CajaVida : Caja
{
    public GameObject vidaPrefab;
    public int vida = 1;

    public override void Romper(TipoImpacto tipo)
    {
        Instantiate(vidaPrefab, transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
    }
}